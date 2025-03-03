﻿using Microsoft.Extensions.Logging;

using System.Collections.Concurrent;

namespace StreamMaster.Streams.Streams;

public sealed class StreamManager(
    IClientStreamerManager clientStreamerManager,
    IStreamHandlerFactory streamHandlerFactory,
    ILogger<StreamManager> logger
    ) : IStreamManager
{

    public event EventHandler<IStreamHandler> OnStreamingStoppedEvent;
    private readonly ConcurrentDictionary<string, IStreamHandler> _streamHandlers = new();
    private readonly object _disposeLock = new();
    private bool _disposed = false;

    public void Dispose()
    {
        lock (_disposeLock)
        {
            if (_disposed)
            {
                return;
            }

            try
            {
                // Dispose of each stream controller
                foreach (IStreamHandler streamHandler in _streamHandlers.Values)
                {
                    try
                    {
                        streamHandler.Stop().Wait();
                        streamHandler.Dispose();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Error disposing stream handler {VideoStreamId}", streamHandler.VideoStreamId);
                    }
                }

                // Clear the dictionary
                _streamHandlers.Clear();

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred during disposing of StreamManager");
            }
            finally
            {
                _disposed = true;
            }
        }
    }

    private async Task<IStreamHandler?> CreateStreamHandler(VideoStreamDto videoStreamDto, string ChannelId, string ChannelName, int rank, CancellationToken cancellation)
    {

        IStreamHandler? streamHandler = await streamHandlerFactory.CreateStreamHandlerAsync(videoStreamDto, ChannelId, ChannelName, rank, cancellation);

        return streamHandler;
    }

    public IStreamHandler? GetStreamHandler(string videoStreamId)
    {
        return !_streamHandlers.TryGetValue(videoStreamId, out IStreamHandler? streamHandler) ? null : streamHandler;
    }

    public async Task<IStreamHandler?> GetOrCreateStreamHandler(VideoStreamDto videoStreamDto, string ChannelId, string ChannelName, int rank, CancellationToken cancellation = default)
    {
        _ = _streamHandlers.TryGetValue(videoStreamDto.User_Url, out IStreamHandler? streamHandler);

        if (streamHandler is not null && streamHandler.IsFailed == true)
        {
            await StopAndUnRegisterHandler(videoStreamDto.User_Url);
            _ = _streamHandlers.TryGetValue(videoStreamDto.User_Url, out streamHandler);
        }

        if (streamHandler is null)
        {
            logger.LogInformation("Creating new handler for stream: {Id} {name}", videoStreamDto.Id, videoStreamDto.User_Tvg_name);
            streamHandler = await CreateStreamHandler(videoStreamDto, ChannelId, ChannelName, rank, cancellation);
            if (streamHandler == null)
            {
                return null;
            }

            streamHandler.OnStreamingStoppedEvent += StreamHandler_OnStreamingStoppedEvent;
            bool test = _streamHandlers.TryAdd(videoStreamDto.User_Url, streamHandler);

            return streamHandler;
        }

        logger.LogInformation("Reusing handler for stream: {Id} {name}", videoStreamDto.Id, videoStreamDto.User_Tvg_name);
        return streamHandler;
    }

    private async void StreamHandler_OnStreamingStoppedEvent(object? sender, StreamHandlerStopped StoppedEvent)
    {
        if (sender is IStreamHandler streamHandler)
        {
            if (streamHandler is not null)
            {
                if (StoppedEvent.InputStreamError && streamHandler.ClientCount > 0)
                {
                    if (await streamHandlerFactory.RestartStreamHandlerAsync(streamHandler).ConfigureAwait(false) == null)
                    {
                        OnStreamingStoppedEvent?.Invoke(sender, streamHandler);
                    }
                    else
                    {
                        //streamHandler.RestartCount = 0;
                        //foreach (Guid clientId in streamHandler.GetClientStreamerClientIds())
                        //{
                        //    IClientStreamerConfiguration? clientStreamerConfiguration = await clientStreamerManager.GetClientStreamerConfiguration(clientId);
                        //    if (clientStreamerConfiguration != null && clientStreamerConfiguration.ReadBuffer != null)
                        //    {
                        //        long _lastReadIndex = streamHandler.CircularRingBuffer.GetNextReadIndex();
                        //        //if (_lastReadIndex > StreamHandler.ChunkSize)
                        //        //{
                        //        //    _lastReadIndex -= StreamHandler.ChunkSize;
                        //        //}
                        //        clientStreamerConfiguration.ReadBuffer.SetLastIndex(_lastReadIndex);
                        //    }
                        //}
                    }
                }
                else
                {
                    OnStreamingStoppedEvent?.Invoke(sender, streamHandler);
                }
            }
        }
    }

    public async Task<VideoInfo> GetVideoInfo(string streamUrl)
    {
        IStreamHandler? handler = GetStreamHandlerFromStreamUrl(streamUrl);
        return handler is null ? new() : handler.GetVideoInfo();
    }

    public IStreamHandler? GetStreamHandlerFromStreamUrl(string streamUrl)
    {
        return _streamHandlers.Values.FirstOrDefault(x => x.StreamUrl == streamUrl);
    }
    public int GetStreamsCountForM3UFile(int m3uFileId)
    {
        return _streamHandlers.Count(x => x.Value.M3UFileId == m3uFileId);
    }

    public List<IStreamHandler> GetStreamHandlers()
    {
        return _streamHandlers.Values == null ? ([]) : ([.. _streamHandlers.Values]);
    }

    public async Task MoveClientStreamers(IStreamHandler oldStreamHandler, IStreamHandler newStreamHandler, CancellationToken cancellationToken = default)
    {
        IEnumerable<Guid> ClientIds = oldStreamHandler.GetClientStreamerClientIds();

        if (!ClientIds.Any())
        {
            return;
        }

        logger.LogInformation("Moving clients {count} from {OldStreamUrl} to {NewStreamUrl}", ClientIds.Count(), oldStreamHandler.VideoStreamName, newStreamHandler.VideoStreamName);


        foreach (Guid clientId in ClientIds)
        {
            IClientStreamerConfiguration? streamerConfiguration = await clientStreamerManager.GetClientStreamerConfiguration(clientId, cancellationToken);

            if (streamerConfiguration == null)
            {
                logger.LogError("Error registering stream configuration for client {ClientId}, streamerConfiguration null.", clientId);
                return;
            }

            await clientStreamerManager.AddClientToHandler(streamerConfiguration.ClientId, newStreamHandler);
            _ = oldStreamHandler.UnRegisterClientStreamer(clientId);

        }


        if (oldStreamHandler.ClientCount == 0)
        {
            //await Task.Delay(100);
            await StopAndUnRegisterHandler(oldStreamHandler.StreamUrl);

        }
    }

    public IStreamHandler? GetStreamHandlerByClientId(Guid ClientId)
    {
        return _streamHandlers.Values.FirstOrDefault(x => x.HasClient(ClientId));
    }

    public async Task<bool> StopAndUnRegisterHandler(string VideoStreamUrl)
    {
        if (_streamHandlers.TryRemove(VideoStreamUrl, out IStreamHandler? streamHandler))
        {
            await streamHandler.Stop();
            return true;
        }

        logger.LogWarning("StopAndUnRegisterHandler {VideoStreamUrl} doesnt exist", VideoStreamUrl);
        return false;

    }

}
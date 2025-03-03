﻿using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using StreamMaster.Domain.Cache;
using StreamMaster.Streams.Streams;
namespace StreamMaster.Streams.Factories;

public sealed class StreamHandlerFactory(ICircularRingBufferFactory circularRingBufferFactory, IClientStreamerManager clientStreamerManager, IMemoryCache memoryCache, ILogger<StreamHandler> streamHandlerLogger, IProxyFactory proxyFactory) : IStreamHandlerFactory
{
    public async Task<IStreamHandler?> CreateStreamHandlerAsync(VideoStreamDto videoStreamDto, string ChannelId, string ChannelName, int rank, CancellationToken cancellationToken)
    {
        (Stream? stream, int processId, ProxyStreamError? error) = await proxyFactory.GetProxy(videoStreamDto.User_Url, videoStreamDto.User_Tvg_name, videoStreamDto.StreamingProxyType, cancellationToken).ConfigureAwait(false);
        if (stream == null || error != null || processId == 0)
        {
            return null;
        }

        ICircularRingBuffer ringBuffer = circularRingBufferFactory.CreateCircularRingBuffer(videoStreamDto, ChannelId, ChannelName, rank);

        StreamHandler streamHandler = new(videoStreamDto, processId, memoryCache, clientStreamerManager, streamHandlerLogger, ringBuffer);

        _ = Task.Run(() => streamHandler.StartVideoStreamingAsync(stream), cancellationToken);
        
        return streamHandler;
    }

    public async Task<IStreamHandler?> RestartStreamHandlerAsync(IStreamHandler StreamHandler)
    {
        Setting setting = memoryCache.GetSetting();

        if (StreamHandler.RestartCount > setting.MaxStreamReStart)
        {
            return null;
        }

        StreamHandler.RestartCount++;

        (Stream? stream, int processId, ProxyStreamError? error) = await proxyFactory.GetProxy(StreamHandler.VideoStreamDto.User_Url, StreamHandler.VideoStreamDto.User_Tvg_name, StreamHandler.VideoStreamDto.StreamingProxyType, CancellationToken.None).ConfigureAwait(false);
        if (stream == null || error != null || processId == 0)
        {
            return null;
        }

        _ = Task.Run(() => StreamHandler.StartVideoStreamingAsync(stream));

        return StreamHandler;
    }
}

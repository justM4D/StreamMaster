﻿using StreamMaster.Domain.Dto;
using StreamMaster.Domain.Models;
using StreamMaster.Domain.Pagination;
using StreamMaster.Domain.Requests;

using StreamMaster.Application.VideoStreams;
using StreamMaster.Application.VideoStreams.Commands;
using StreamMaster.Application.VideoStreams.Queries;

namespace StreamMaster.Application.Hubs;

public partial class StreamMasterHub : IVideoStreamHub
{
    public async Task CreateVideoStream(CreateVideoStreamRequest request)
    {
        await mediator.Send(request).ConfigureAwait(false);
    }

    public async Task ChangeVideoStreamChannel(ChangeVideoStreamChannelRequest request)
    {
        await mediator.Send(request).ConfigureAwait(false);
    }

    public async Task DeleteVideoStream(DeleteVideoStreamRequest request)
    {
        _ = await mediator.Send(request).ConfigureAwait(false);
    }

    public async Task<VideoStreamDto?> GetVideoStream(string id)
    {
        return await mediator.Send(new GetVideoStream(id)).ConfigureAwait(false);
    }

    public async Task<PagedResponse<VideoStreamDto>> GetPagedVideoStreams(VideoStreamParameters Parameters)
    {
        return await mediator.Send(new GetPagedVideoStreams(Parameters)).ConfigureAwait(false);
    }

    public async Task ReSetVideoStreamsLogo(ReSetVideoStreamsLogoRequest request)
    {
        await mediator.Send(request).ConfigureAwait(false);
    }

    public async Task SetVideoStreamChannelNumbers(SetVideoStreamChannelNumbersRequest request)
    {
        await mediator.Send(request).ConfigureAwait(false);
    }

    public async Task SetVideoStreamSetEPGsFromName(SetVideoStreamSetEPGsFromNameRequest request)
    {
        _ = await mediator.Send(request).ConfigureAwait(false);
    }

    public async Task SetVideoStreamsLogoFromEPG(SetVideoStreamsLogoFromEPGRequest request)
    {
        await mediator.Send(request).ConfigureAwait(false);
    }

    public async Task UpdateVideoStream(UpdateVideoStreamRequest request)
    {
        _ = await mediator.Send(request).ConfigureAwait(false);
    }

    public async Task UpdateVideoStreams(UpdateVideoStreamsRequest request)
    {
        await mediator.Send(request).ConfigureAwait(false);
    }

    public async Task<IEnumerable<VideoStreamDto>> GetVideoStreamsByNamePattern(GetVideoStreamsByNamePatternQuery request)
    {
        return await mediator.Send(request).ConfigureAwait(false);
    }

    public async Task<IEnumerable<string>> GetVideoStreamNamesByNamePattern(GetVideoStreamNamesByNamePatternQuery request)
    {
        return await mediator.Send(request).ConfigureAwait(false);
    }

    public async Task UpdateAllVideoStreamsFromParameters(UpdateAllVideoStreamsFromParametersRequest request)
    {
        await mediator.Send(request).ConfigureAwait(false);
    }

    public async Task DeleteAllVideoStreamsFromParameters(DeleteAllVideoStreamsFromParametersRequest request)
    {
        await mediator.Send(request).ConfigureAwait(false);
    }

    public async Task SetVideoStreamChannelNumbersFromParameters(SetVideoStreamChannelNumbersFromParametersRequest request)
    {
        await mediator.Send(request).ConfigureAwait(false);
    }

    public async Task SetVideoStreamsLogoFromEPGFromParameters(SetVideoStreamsLogoFromEPGFromParametersRequest request)
    {
        await mediator.Send(request).ConfigureAwait(false);
    }

    public async Task ReSetVideoStreamsLogoFromParameters(ReSetVideoStreamsLogoFromParametersRequest request)
    {
        await mediator.Send(request).ConfigureAwait(false);
    }

    public async Task AutoSetEPG(AutoSetEPGRequest request)
    {
        await mediator.Send(request).ConfigureAwait(false);
    }

    public async Task AutoSetEPGFromParameters(AutoSetEPGFromParametersRequest request)
    {
        await mediator.Send(request).ConfigureAwait(false);
    }

    public async Task SetVideoStreamTimeShifts(SetVideoStreamTimeShiftsRequest request)
    {
        await mediator.Send(request).ConfigureAwait(false);
    }

    public async Task SetVideoStreamTimeShiftFromParameters(SetVideoStreamTimeShiftFromParametersRequest request)
    {
        await mediator.Send(request).ConfigureAwait(false);
    }

    public async Task<List<IdName>> GetVideoStreamNames()
    {
        return await mediator.Send(new GetVideoStreamNamesRequest()).ConfigureAwait(false);
    }


    public async Task<VideoInfo> GetVideoStreamInfoFromId(string channelVideoStreamId)
    {
        return await mediator.Send(new GetVideoStreamInfoFromIdRequest(channelVideoStreamId)).ConfigureAwait(false);
    }

    public async Task<VideoInfo> GetVideoStreamInfoFromUrl(string channelVideoStreamId)
    {
        return await mediator.Send(new GetVideoStreamInfoFromUrlRequest(channelVideoStreamId)).ConfigureAwait(false);

    }
}
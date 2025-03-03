﻿using StreamMaster.Application.Icons;
using StreamMaster.Application.Icons.Commands;
using StreamMaster.Application.Icons.Queries;
using StreamMaster.Domain.Pagination;

namespace StreamMaster.Application.Hubs;

public partial class StreamMasterHub : IIconHub
{

    public async Task AutoMatchIconToStreams(AutoMatchIconToStreamsRequest request)
    {
        await mediator.Send(request).ConfigureAwait(false);
    }
    public async Task<IconFileDto?> GetIconFromSource(GetIconFromSourceRequest request)
    {
        return await mediator.Send(request).ConfigureAwait(false);
    }


    public async Task<PagedResponse<IconFileDto>> GetPagedIcons(IconFileParameters iconFileParameters)
    {
        PagedResponse<IconFileDto> data = await mediator.Send(new GetPagedIcons(iconFileParameters)).ConfigureAwait(false);
        return data;
    }

    public async Task<IEnumerable<IconFileDto>> GetIconsSimpleQuery(IconFileParameters iconFileParameters)
    {
        IEnumerable<IconFileDto> data = await mediator.Send(new GetIconsSimpleQuery(iconFileParameters)).ConfigureAwait(false);
        return data;
    }
}
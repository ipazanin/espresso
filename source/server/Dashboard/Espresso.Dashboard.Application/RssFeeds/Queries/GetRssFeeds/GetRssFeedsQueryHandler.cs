// GetRssFeedsQueryHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.RssFeeds.Queries.GetRssFeeds;

public class GetRssFeedsQueryHandler : IRequestHandler<GetRssFeedsQuery, GetRssFeedsQueryResponse>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;

    public GetRssFeedsQueryHandler(IEspressoDatabaseContext espressoDatabaseContext)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
    }

    public async Task<GetRssFeedsQueryResponse> Handle(GetRssFeedsQuery request, CancellationToken cancellationToken)
    {
        var rssFeeds = await _espressoDatabaseContext
            .RssFeeds
            .AsNoTracking()
            .FilterRssFeeds(request.PagingParameters)
            .OrderRssFeeds(request.PagingParameters)
            .Skip(request.PagingParameters.GetSkip())
            .Take(request.PagingParameters.GetTake())
            .Select(GetRssFeedsQueryRssFeed.Projection)
            .ToArrayAsync(cancellationToken);

        var totalCount = await _espressoDatabaseContext
            .RssFeeds
            .AsNoTracking()
            .FilterRssFeeds(request.PagingParameters)
            .CountAsync(cancellationToken);

        var pagingMetadata = new PagingMetadata(
            currentPage: request.PagingParameters.CurrentPage,
            pageSize: request.PagingParameters.PageSize,
            totalCount: totalCount);

        var pagedList = new PagedList<GetRssFeedsQueryRssFeed>(items: rssFeeds, pagingMetadata: pagingMetadata);

        return new GetRssFeedsQueryResponse(pagedList);
    }
}

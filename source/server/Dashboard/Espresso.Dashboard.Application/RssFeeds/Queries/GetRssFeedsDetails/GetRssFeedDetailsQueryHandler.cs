// GetRssFeedDetailsQueryHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CategoryDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects.RssFeedDataTransferObjects;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.RssFeeds.Queries.GetRssFeedsDetails;

public class GetRssFeedDetailsQueryHandler : IRequestHandler<GetRssFeedDetailsQuery, GetRssFeedDetailsQueryResponse>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;

    public GetRssFeedDetailsQueryHandler(IEspressoDatabaseContext espressoDatabaseContext)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
    }

    public async Task<GetRssFeedDetailsQueryResponse> Handle(
        GetRssFeedDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var rssFeed = await _espressoDatabaseContext
            .RssFeeds
            .AsNoTracking()
            .OrderBy(rssFeed => rssFeed.Id)
            .Select(RssFeedDto.GetProjection())
            .FirstAsync(rssFeed => rssFeed.Id == request.RssFeedId, cancellationToken);

        var rssFeedCategories = await _espressoDatabaseContext
            .RssFeedCategory
            .AsNoTracking()
            .OrderBy(rssFeedCategory => rssFeedCategory.Id)
            .Where(rssFeedCategory => rssFeedCategory.RssFeedId == rssFeed.Id)
            .Select(RssFeedCategoryDto.Projection)
            .ToListAsync(cancellationToken);

        var rssFeedContentModifiers = await _espressoDatabaseContext
            .RssFeedContentModifiers
            .AsNoTracking()
            .OrderByDescending(rssFeedCategory => rssFeedCategory.OrderIndex)
            .Where(rssFeedContentModifier => rssFeedContentModifier.RssFeedId == rssFeed.Id)
            .Select(RssFeedContentModifierDto.Projection)
            .ToListAsync(cancellationToken);

        var newsPortals = await _espressoDatabaseContext
            .NewsPortals
            .AsNoTracking()
            .OrderBy(newsPortal => newsPortal.Name)
            .Select(NewsPortalDto.GetProjection())
            .ToArrayAsync(cancellationToken);

        var categories = await _espressoDatabaseContext
            .Categories
            .AsNoTracking()
            .OrderBy(category => category.Name)
            .Select(CategoryDto.Projection)
            .ToArrayAsync(cancellationToken);

        return new GetRssFeedDetailsQueryResponse(
            rssFeed: rssFeed,
            newsPortals: newsPortals,
            categories: categories,
            rssFeedCategories: rssFeedCategories,
            rssFeedContentModifiers: rssFeedContentModifiers);
    }
}

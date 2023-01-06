// GetNewsPortalDetailsQueryHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CategoryDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects.RssFeedDataTransferObjects;
using Espresso.Dashboard.Application.NewsPortals.Queries.GetNewsPortalDetails;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.NewsPortals.GetNewsPortalDetails;

/// <summary>
/// Get <see cref="NewsPortal"/> details query handler.
/// </summary>
public class GetNewsPortalDetailsQueryHandler : IRequestHandler<GetNewsPortalDetailsQuery, GetNewsPortalDetailsQueryResponse>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetNewsPortalDetailsQueryHandler"/> class.
    /// </summary>
    /// <param name="espressoDatabaseContext">Espresso database context.</param>
    public GetNewsPortalDetailsQueryHandler(IEspressoDatabaseContext espressoDatabaseContext)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
    }

    /// <inheritdoc/>
    public async Task<GetNewsPortalDetailsQueryResponse> Handle(GetNewsPortalDetailsQuery request, CancellationToken cancellationToken)
    {
        var newsPortal = await _espressoDatabaseContext
            .NewsPortals
            .AsNoTracking()
            .Select(NewsPortalDto.GetProjection())
            .FirstAsync(
                predicate: newsPortal => newsPortal.Id == request.NewsPortalId,
                cancellationToken: cancellationToken);

        var categories = await _espressoDatabaseContext
            .Categories
            .AsNoTracking()
            .AsSplitQuery()
            .Select(CategoryDto.Projection)
            .ToArrayAsync(cancellationToken);

        var regions = await _espressoDatabaseContext
            .Regions
            .AsNoTracking()
            .AsSplitQuery()
            .Select(RegionDto.Projection)
            .ToArrayAsync(cancellationToken);

        var rssFeeds = await _espressoDatabaseContext
            .RssFeeds
            .AsNoTracking()
            .AsSplitQuery()
            .Where(rssFeed => rssFeed.NewsPortalId == request.NewsPortalId)
            .Select(RssFeedDto.GetProjection())
            .ToArrayAsync(cancellationToken);

        return new GetNewsPortalDetailsQueryResponse(
            newsPortal: newsPortal,
            categories: categories,
            regions: regions,
            rssFeeds: rssFeeds);
    }
}

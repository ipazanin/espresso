// ExportDatabaseQueryHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CategoryDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects.RssFeedDataTransferObjects;
using Espresso.Application.DataTransferObjects.SettingDataTransferObjects;
using Espresso.Domain.Infrastructure;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.Settings.ExportDatabase;

public class ExportDatabaseQueryHandler : IRequestHandler<ExportDatabaseQuery, ExportDatabaseQueryResponse>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;
    private readonly ISettingProvider _settingProvider;

    public ExportDatabaseQueryHandler(
        IEspressoDatabaseContext espressoDatabaseContext,
        ISettingProvider settingProvider)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
        _settingProvider = settingProvider;
    }

    public async Task<ExportDatabaseQueryResponse> Handle(ExportDatabaseQuery request, CancellationToken cancellationToken)
    {
        var latestSetting = _settingProvider.LatestSetting;

        var settingDto = SettingDto.Projection.Compile().Invoke(latestSetting);

        var newsPortals = await _espressoDatabaseContext
            .NewsPortals
            .AsNoTracking()
            .OrderBy(newsPortal => newsPortal.Name)
            .Select(NewsPortalDto.GetProjection())
            .ToArrayAsync(cancellationToken);

        var regions = await _espressoDatabaseContext
            .Regions
            .AsNoTracking()
            .OrderBy(region => region.Name)
            .Select(RegionDto.Projection)
            .ToArrayAsync(cancellationToken);

        var categories = await _espressoDatabaseContext
            .Categories
            .OrderBy(category => category.Name)
            .AsNoTracking()
            .Select(CategoryDto.Projection)
            .ToArrayAsync(cancellationToken);

        var rssFeeds = await _espressoDatabaseContext
            .RssFeeds
            .AsNoTracking()
            .OrderBy(rssFeed => rssFeed.Id)
            .Select(RssFeedDto.GetProjection())
            .ToArrayAsync(cancellationToken);

        var rssFeedContentModifiers = await _espressoDatabaseContext
            .RssFeedContentModifiers
            .AsNoTracking()
            .OrderBy(rssFeedContentModifier => rssFeedContentModifier.Id)
            .Select(RssFeedContentModifierDto.Projection)
            .ToArrayAsync(cancellationToken);

        var rssFeedCategories = await _espressoDatabaseContext
            .RssFeedCategory
            .AsNoTracking()
            .OrderBy(rssFeedCategory => rssFeedCategory.Id)
            .Select(RssFeedCategoryDto.Projection)
            .ToArrayAsync(cancellationToken);

        var newsPortalImages = await _espressoDatabaseContext
            .NewsPortalImages
            .AsNoTracking()
            .AsSplitQuery()
            .OrderBy(newsPortalImage => newsPortalImage.Id)
            .Select(NewsPortalImageDto.GetProjection())
            .ToArrayAsync(cancellationToken);

        return new ExportDatabaseQueryResponse(
            setting: settingDto,
            newsPortals: newsPortals,
            regions: regions,
            categories: categories,
            rssFeeds: rssFeeds,
            rssFeedContentModifiers: rssFeedContentModifiers,
            rssFeedCategories: rssFeedCategories,
            newsPortalImages: newsPortalImages);
    }
}

// ImportDatabaseCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CategoryDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects.RssFeedDataTransferObjects;
using Espresso.Application.DataTransferObjects.SettingDataTransferObjects;
using Espresso.Dashboard.Application.IServices;
using Espresso.Dashboard.Application.Settings.ImportDatabase;
using Espresso.Domain.Infrastructure;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.Settings.Commands.ImportDatabase;

public class ImportDatabaseCommandHandler : IRequestHandler<ImportDatabaseCommand>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;
    private readonly ISettingProvider _settingProvider;
    private readonly ISendInformationToApiService _sendInformationToApiService;
    private readonly IRefreshDashboardCacheService _refreshDashboardCacheService;

    public ImportDatabaseCommandHandler(
        IEspressoDatabaseContext espressoDatabaseContext,
        ISettingProvider settingProvider,
        ISendInformationToApiService sendInformationToApiService,
        IRefreshDashboardCacheService refreshDashboardCacheService)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
        _settingProvider = settingProvider;
        _sendInformationToApiService = sendInformationToApiService;
        _refreshDashboardCacheService = refreshDashboardCacheService;
    }

    public async Task Handle(ImportDatabaseCommand request, CancellationToken cancellationToken)
    {
        await ImportSetting(request.Setting, cancellationToken);
        await _settingProvider.UpdateLatestSetting(cancellationToken);
        await _sendInformationToApiService.SendSettingUpdatedNotification();

        await ImportCategories(request.Categories, cancellationToken);
        await ImportRegions(request.Regions, cancellationToken);
        await ImportNewsPortals(request.NewsPortals, cancellationToken);
        await ImportRssFeeds(request.RssFeeds, cancellationToken);
        await ImportRssFeedContentModifiers(request.RssFeedContentModifiers, cancellationToken);
        await ImportRssFeedCategoriesModifiers(request.RssFeedCategories, cancellationToken);
        await ImportNewsPortalImagesModifiers(request.NewsPortalImages, cancellationToken);

        await _sendInformationToApiService.SendCacheUpdatedNotification();
        await _refreshDashboardCacheService.RefreshCache();
    }

    private async Task ImportSetting(SettingDto settingDto, CancellationToken cancellationToken)
    {
        var setting = settingDto.CreateSetting();

        _ = _espressoDatabaseContext.Settings.Add(setting);

        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);
    }

    private async Task ImportCategories(IEnumerable<CategoryDto> categoryDataTransferObjects, CancellationToken cancellationToken)
    {
        var categories = categoryDataTransferObjects
            .Select(categoryDto => categoryDto.CreateCategory())
            .ToArray();

        var databaseCategoriesDictionary = await _espressoDatabaseContext
            .Categories
            .ToDictionaryAsync(category => category.Id, cancellationToken);

        foreach (var category in categories)
        {
            if (databaseCategoriesDictionary.ContainsKey(category.Id))
            {
                _ = _espressoDatabaseContext.Categories.Update(category);
            }
            else
            {
                _ = _espressoDatabaseContext.Categories.Add(category);
            }
        }

        var importedCategoryIds = categories.Select(category => category.Id).ToHashSet();

        foreach (var (categoryId, category) in databaseCategoriesDictionary)
        {
            if (importedCategoryIds.Contains(categoryId))
            {
                continue;
            }

            _ = _espressoDatabaseContext.Categories.Remove(category);
        }

        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);
    }

    private async Task ImportRegions(IEnumerable<RegionDto> regionDataTransferObjects, CancellationToken cancellationToken)
    {
        var regions = regionDataTransferObjects
            .Select(regionDto => regionDto.CreateRegion())
            .ToArray();

        var databaseRegionsDictionary = await _espressoDatabaseContext
            .Regions
            .ToDictionaryAsync(region => region.Id, cancellationToken);

        foreach (var region in regions)
        {
            if (databaseRegionsDictionary.ContainsKey(region.Id))
            {
                _ = _espressoDatabaseContext.Regions.Update(region);
            }
            else
            {
                _ = _espressoDatabaseContext.Regions.Add(region);
            }
        }

        var importedRegionIds = regions.Select(region => region.Id).ToHashSet();

        foreach (var (regionId, region) in databaseRegionsDictionary)
        {
            if (importedRegionIds.Contains(regionId))
            {
                continue;
            }

            _ = _espressoDatabaseContext.Regions.Remove(region);
        }

        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);
    }

    private async Task ImportNewsPortals(IEnumerable<NewsPortalDto> newsPortalDataTransferObjects, CancellationToken cancellationToken)
    {
        var newsPortals = newsPortalDataTransferObjects
            .Select(newsPortalDto => newsPortalDto.CreateNewsPortal())
            .ToArray();

        var databaseNewsPortalsDictionary = await _espressoDatabaseContext
            .NewsPortals
            .ToDictionaryAsync(newsPortal => newsPortal.Id, cancellationToken);

        foreach (var newsPortal in newsPortals)
        {
            if (databaseNewsPortalsDictionary.ContainsKey(newsPortal.Id))
            {
                _ = _espressoDatabaseContext.NewsPortals.Update(newsPortal);
            }
            else
            {
                _ = _espressoDatabaseContext.NewsPortals.Add(newsPortal);
            }
        }

        var importedNewsPortalIds = newsPortals.Select(newsPortal => newsPortal.Id).ToHashSet();

        foreach (var (newsPortalId, newsPortal) in databaseNewsPortalsDictionary)
        {
            if (importedNewsPortalIds.Contains(newsPortalId))
            {
                continue;
            }

            _ = _espressoDatabaseContext.NewsPortals.Remove(newsPortal);
        }

        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);
    }

    private async Task ImportRssFeeds(IEnumerable<RssFeedDto> rssFeedDataTransferObjects, CancellationToken cancellationToken)
    {
        var rssFeeds = rssFeedDataTransferObjects
            .Select(rssFeedDto => rssFeedDto.CreateRssFeed())
            .ToArray();

        var databaseRssFeedsDictionary = await _espressoDatabaseContext
            .RssFeeds
            .ToDictionaryAsync(rssFeed => rssFeed.Id, cancellationToken);

        foreach (var rssFeed in rssFeeds)
        {
            if (databaseRssFeedsDictionary.ContainsKey(rssFeed.Id))
            {
                _ = _espressoDatabaseContext.RssFeeds.Update(rssFeed);
            }
            else
            {
                _ = _espressoDatabaseContext.RssFeeds.Add(rssFeed);
            }
        }

        var importedRssFeedIds = rssFeeds.Select(rssFeed => rssFeed.Id).ToHashSet();

        foreach (var (rssFeedId, rssFeed) in databaseRssFeedsDictionary)
        {
            if (importedRssFeedIds.Contains(rssFeedId))
            {
                continue;
            }

            _ = _espressoDatabaseContext.RssFeeds.Remove(rssFeed);
        }

        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);
    }

    private async Task ImportRssFeedContentModifiers(
        IEnumerable<RssFeedContentModifierDto> rssFeedContentModifierDataTransferObjects,
        CancellationToken cancellationToken)
    {
        var rssFeedContentModifiers = rssFeedContentModifierDataTransferObjects
            .Select(rssFeedContentModifierDto => rssFeedContentModifierDto.CreateRssFeedContentModifier())
            .ToArray();

        var databaseRssFeedsDictionary = await _espressoDatabaseContext
            .RssFeedContentModifiers
            .ToDictionaryAsync(rssFeedContentModifier => rssFeedContentModifier.Id, cancellationToken);

        foreach (var rssFeedContentModifier in rssFeedContentModifiers)
        {
            if (databaseRssFeedsDictionary.ContainsKey(rssFeedContentModifier.Id))
            {
                _ = _espressoDatabaseContext.RssFeedContentModifiers.Update(rssFeedContentModifier);
            }
            else
            {
                _ = _espressoDatabaseContext.RssFeedContentModifiers.Add(rssFeedContentModifier);
            }
        }

        var importedRssFeedContentModifiersIds = rssFeedContentModifiers
            .Select(rssFeedContentModifier => rssFeedContentModifier.Id)
            .ToHashSet();

        foreach (var (rssFeedContentModifierId, rssFeedContentModifier) in databaseRssFeedsDictionary)
        {
            if (importedRssFeedContentModifiersIds.Contains(rssFeedContentModifierId))
            {
                continue;
            }

            _ = _espressoDatabaseContext.RssFeedContentModifiers.Remove(rssFeedContentModifier);
        }

        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);
    }

    private async Task ImportRssFeedCategoriesModifiers(
        IEnumerable<RssFeedCategoryDto> rssFeedContentModifierDataTransferObjects,
        CancellationToken cancellationToken)
    {
        var rssFeedCategories = rssFeedContentModifierDataTransferObjects
            .Select(rssFeedCategoryDto => rssFeedCategoryDto.CreateRssFeedCategory())
            .ToArray();

        var databaseRssFeedsDictionary = await _espressoDatabaseContext
            .RssFeedCategory
            .ToDictionaryAsync(rssFeedCategory => rssFeedCategory.Id, cancellationToken);

        foreach (var rssFeedCategory in rssFeedCategories)
        {
            if (databaseRssFeedsDictionary.ContainsKey(rssFeedCategory.Id))
            {
                _ = _espressoDatabaseContext.RssFeedCategory.Update(rssFeedCategory);
            }
            else
            {
                _ = _espressoDatabaseContext.RssFeedCategory.Add(rssFeedCategory);
            }
        }

        var importedRssFeedCategoryIds = rssFeedCategories
            .Select(rssFeedCategory => rssFeedCategory.Id)
            .ToHashSet();

        foreach (var (rssFeedCategoryId, rssFeedCategory) in databaseRssFeedsDictionary)
        {
            if (importedRssFeedCategoryIds.Contains(rssFeedCategoryId))
            {
                continue;
            }

            _ = _espressoDatabaseContext.RssFeedCategory.Remove(rssFeedCategory);
        }

        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);
    }

    private async Task ImportNewsPortalImagesModifiers(
        IEnumerable<NewsPortalImageDto> newsPortalImageDataTransferObjects,
        CancellationToken cancellationToken)
    {
        var newsPortalImages = newsPortalImageDataTransferObjects
            .Select(newsPortalImageDto => newsPortalImageDto.CreateNewsPortalImage())
            .ToArray();

        var databaseNewsPortalImagesDictionary = await _espressoDatabaseContext
            .NewsPortalImages
            .ToDictionaryAsync(newsPortalImage => newsPortalImage.Id, cancellationToken);

        foreach (var newsPortalImage in newsPortalImages)
        {
            if (databaseNewsPortalImagesDictionary.ContainsKey(newsPortalImage.Id))
            {
                _ = _espressoDatabaseContext.NewsPortalImages.Update(newsPortalImage);
            }
            else
            {
                _ = _espressoDatabaseContext.NewsPortalImages.Add(newsPortalImage);
            }
        }

        var importedNewsPortalImageIds = newsPortalImages
            .Select(newsPortalImage => newsPortalImage.Id)
            .ToHashSet();

        foreach (var (newsPortalImageId, newsPortalImage) in databaseNewsPortalImagesDictionary)
        {
            if (importedNewsPortalImageIds.Contains(newsPortalImageId))
            {
                continue;
            }

            _ = _espressoDatabaseContext.NewsPortalImages.Remove(newsPortalImage);
        }

        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);
    }
}

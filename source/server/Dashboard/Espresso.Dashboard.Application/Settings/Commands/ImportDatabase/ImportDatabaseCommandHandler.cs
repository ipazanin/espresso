// ImportDatabaseCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.SettingDataTransferObjects;
using Espresso.Dashboard.Application.IServices;
using Espresso.Dashboard.Application.Settings.ImportDatabase;
using Espresso.Domain.Entities;
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

    public async Task<Unit> Handle(ImportDatabaseCommand request, CancellationToken cancellationToken)
    {
        await ImportSetting(request.Setting, cancellationToken);
        await _settingProvider.UpdateLatestSetting(cancellationToken);
        await _sendInformationToApiService.SendSettingUpdatedNotification();

        await ImportEntities<Category, int>(
            entities: request.Categories.Select(categoryDto => categoryDto.CreateCategory()).ToArray(),
            entitiesDatabaseSet: _espressoDatabaseContext.Categories,
            cancellationToken: cancellationToken);
        await ImportEntities<Region, int>(
            entities: request.Regions.Select(regionDto => regionDto.CreateRegion()).ToArray(),
            entitiesDatabaseSet: _espressoDatabaseContext.Regions,
            cancellationToken: cancellationToken);
        await ImportEntities<NewsPortal, int>(
            entities: request.NewsPortals.Select(newsPortalDto => newsPortalDto.CreateNewsPortal()).ToArray(),
            entitiesDatabaseSet: _espressoDatabaseContext.NewsPortals,
            cancellationToken: cancellationToken);
        await ImportEntities<RssFeed, int>(
            entities: request.RssFeeds.Select(rssFeedDto => rssFeedDto.CreateRssFeed()).ToArray(),
            entitiesDatabaseSet: _espressoDatabaseContext.RssFeeds,
            cancellationToken: cancellationToken);
        await ImportEntities<RssFeedCategory, int>(
            entities: request.RssFeedCategories.Select(rssFeedCategoryDto => rssFeedCategoryDto.CreateRssFeedCategory()).ToArray(),
            entitiesDatabaseSet: _espressoDatabaseContext.RssFeedCategory,
            cancellationToken: cancellationToken);
        await ImportEntities<RssFeedContentModifier, int>(
            entities: request.RssFeedContentModifiers.Select(rssFeedContentModifierDto => rssFeedContentModifierDto.CreateRssFeedContentModifier()).ToArray(),
            entitiesDatabaseSet: _espressoDatabaseContext.RssFeedContentModifiers,
            cancellationToken: cancellationToken);
        await ImportEntities<NewsPortalImage, int>(
            entities: request.NewsPortalImages.Select(newsPortalImageDto => newsPortalImageDto.CreateNewsPortalImage()).ToArray(),
            entitiesDatabaseSet: _espressoDatabaseContext.NewsPortalImages,
            cancellationToken: cancellationToken);
        await ImportEntities<Country, int>(
            entities: request.Countries.Select(countryDto => countryDto.CreateCountry()).ToArray(),
            entitiesDatabaseSet: _espressoDatabaseContext.Countries,
            cancellationToken: cancellationToken);
        await ImportEntities<CountryImage, int>(
            entities: request.CountryImages.Select(countryImageDto => countryImageDto.CreateCountryImage()).ToArray(),
            entitiesDatabaseSet: _espressoDatabaseContext.CountryImages,
            cancellationToken: cancellationToken);

        await _sendInformationToApiService.SendCacheUpdatedNotification();
        await _refreshDashboardCacheService.RefreshCache();

        return Unit.Value;
    }

    private async Task ImportSetting(SettingDto settingDto, CancellationToken cancellationToken)
    {
        var setting = settingDto.CreateSetting();

        _ = _espressoDatabaseContext.Settings.Add(setting);

        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);
    }

    private async Task ImportEntities<TEntity, TKey>(
        IReadOnlyList<TEntity> entities,
        DbSet<TEntity> entitiesDatabaseSet,
        CancellationToken cancellationToken)
        where TEntity : class, IEntity<TKey>
        where TKey : notnull
    {
        var databaseEntitiesDictionary = await entitiesDatabaseSet.ToDictionaryAsync(entity => entity.Id, cancellationToken);

        foreach (var entity in entities)
        {
            if (databaseEntitiesDictionary.ContainsKey(entity.Id))
            {
                _ = entitiesDatabaseSet.Update(entity);
            }
            else
            {
                _ = entitiesDatabaseSet.Add(entity);
            }
        }

        var importedEntityIds = entities.Select(entity => entity.Id).ToHashSet();

        foreach (var (id, entity) in databaseEntitiesDictionary)
        {
            if (importedEntityIds.Contains(id))
            {
                continue;
            }

            _ = entitiesDatabaseSet.Remove(entity);
        }

        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);
    }
}

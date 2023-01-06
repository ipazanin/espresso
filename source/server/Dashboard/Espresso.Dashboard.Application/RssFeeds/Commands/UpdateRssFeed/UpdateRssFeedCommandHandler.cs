// UpdateRssFeedCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Dashboard.Application.IServices;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.RssFeeds.Commands.UpdateRssFeed;

public class UpdateRssFeedCommandHandler : IRequestHandler<UpdateRssFeedCommand>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;
    private readonly ISendInformationToApiService _sendInformationToApiService;
    private readonly IRefreshDashboardCacheService _refreshDashboardCacheService;

    public UpdateRssFeedCommandHandler(
        IEspressoDatabaseContext espressoDatabaseContext,
        ISendInformationToApiService sendInformationToApiService,
        IRefreshDashboardCacheService refreshDashboardCacheService)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
        _sendInformationToApiService = sendInformationToApiService;
        _refreshDashboardCacheService = refreshDashboardCacheService;
    }

    public async Task<Unit> Handle(UpdateRssFeedCommand request, CancellationToken cancellationToken)
    {
        var rssFeed = request.RssFeed.CreateRssFeed();

        var importedRssFeedCategories = request
            .RssFeedCategories
            .Select(rssFeedCategory => rssFeedCategory.CreateRssFeedCategory());
        await ImportRssFeedCategories(importedRssFeedCategories, rssFeed.Id, cancellationToken);

        var importedRssFeedContentModifiers = request
            .RssFeedContentModifiers
            .Select(rssFeedContentModifier => rssFeedContentModifier.CreateRssFeedContentModifier());
        await ImportRssFeedContentModifiers(importedRssFeedContentModifiers, rssFeed.Id, cancellationToken);
        _ = _espressoDatabaseContext.RssFeeds.Update(rssFeed);

        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

        await _sendInformationToApiService.SendCacheUpdatedNotification();
        await _refreshDashboardCacheService.RefreshCache();

        return Unit.Value;
    }

    private async Task ImportRssFeedCategories(
        IEnumerable<RssFeedCategory> importedRssFeedCategories,
        int rssFeedId,
        CancellationToken cancellationToken)
    {
        var databaseRssFeedCategories = await _espressoDatabaseContext
            .RssFeedCategory
            .Where(rssFeedCategory => rssFeedCategory.RssFeedId == rssFeedId)
            .ToArrayAsync(cancellationToken);

        var databaseRssFeedCategoryIds = databaseRssFeedCategories
            .Select(rssFeedCategory => rssFeedCategory.Id)
            .ToHashSet();

        var importedRssFeedCategoryIds = importedRssFeedCategories
            .Select(rssFeedCategory => rssFeedCategory.Id)
            .ToHashSet();

        foreach (var importedRssFeedCategory in importedRssFeedCategories)
        {
            if (databaseRssFeedCategoryIds.Contains(importedRssFeedCategory.Id))
            {
                _ = _espressoDatabaseContext
                    .RssFeedCategory
                    .Update(importedRssFeedCategory);
            }
            else
            {
                _ = _espressoDatabaseContext
                    .RssFeedCategory
                    .Add(importedRssFeedCategory);
            }
        }

        foreach (var databaseRssFeedCategory in databaseRssFeedCategories)
        {
            if (importedRssFeedCategoryIds.Contains(databaseRssFeedCategory.Id))
            {
                continue;
            }

            _ = _espressoDatabaseContext
                .RssFeedCategory
                .Remove(databaseRssFeedCategory);
        }
    }

    private async Task ImportRssFeedContentModifiers(
        IEnumerable<RssFeedContentModifier> importedRssFeedContentModifiers,
        int rssFeedId,
        CancellationToken cancellationToken)
    {
        var databaseRssFeedContentModifiers = await _espressoDatabaseContext
            .RssFeedContentModifiers
            .Where(rssFeedContentModifier => rssFeedContentModifier.RssFeedId == rssFeedId)
            .ToArrayAsync(cancellationToken);

        var databaseRssFeedContentModifierIds = databaseRssFeedContentModifiers
            .Select(rssFeedContentModifier => rssFeedContentModifier.Id)
            .ToHashSet();

        var importedRssFeedContentModifierIds = importedRssFeedContentModifiers
            .Select(rssFeedContentModifier => rssFeedContentModifier.Id)
            .ToHashSet();

        foreach (var importedRssFeedContentModifier in importedRssFeedContentModifiers)
        {
            if (databaseRssFeedContentModifierIds.Contains(importedRssFeedContentModifier.Id))
            {
                _ = _espressoDatabaseContext
                    .RssFeedContentModifiers
                    .Update(importedRssFeedContentModifier);
            }
            else
            {
                _ = _espressoDatabaseContext
                    .RssFeedContentModifiers
                    .Add(importedRssFeedContentModifier);
            }
        }

        foreach (var databaseRssFeedContentModifier in databaseRssFeedContentModifiers)
        {
            if (importedRssFeedContentModifierIds.Contains(databaseRssFeedContentModifier.Id))
            {
                continue;
            }

            _ = _espressoDatabaseContext
                .RssFeedContentModifiers
                .Remove(databaseRssFeedContentModifier);
        }
    }
}

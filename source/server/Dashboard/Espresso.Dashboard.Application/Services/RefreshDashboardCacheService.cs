// RefreshDashboardCacheService.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Diagnostics;
using Espresso.Application.Services.Contracts;
using Espresso.Common.Constants;
using Espresso.Dashboard.Application.IServices;
using Espresso.Domain.IServices;
using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Espresso.Dashboard.Application.Services;

public class RefreshDashboardCacheService : IRefreshDashboardCacheService
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;
    private readonly IMemoryCache _memoryCache;
    private readonly INewsPortalImagesService _newsPortalImagesService;
    private readonly ILoggerService<RefreshDashboardCacheService> _loggerService;

    public RefreshDashboardCacheService(
        IEspressoDatabaseContext espressoDatabaseContext,
        IMemoryCache memoryCache,
        INewsPortalImagesService newsPortalImagesService,
        ILoggerService<RefreshDashboardCacheService> loggerService)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
        _memoryCache = memoryCache;
        _newsPortalImagesService = newsPortalImagesService;
        _loggerService = loggerService;
    }

    public async Task RefreshCache()
    {
        var stopwatch = Stopwatch.StartNew();

        await RefreshMemoryCacheFromDatabase();

        await _newsPortalImagesService.LoadImagesAndSaveToRootFolder();

        stopwatch.Stop();

        var duration = stopwatch.Elapsed;

        var arguments = new List<(string parameterName, object parameterValue)>
        {
            (nameof(duration), duration),
        };

        const string EventName = "Dashboard Memory Cache Refresh";
        _loggerService.Log(EventName, LogLevel.Information, arguments);
    }

    private async Task RefreshMemoryCacheFromDatabase()
    {
        await _espressoDatabaseContext.Database.MigrateAsync();

        var articles = await _espressoDatabaseContext
            .Articles
            .AsNoTracking()
            .Include(article => article.ArticleCategories)
            .AsSplitQuery()
            .ToArrayAsync();

        var rssFeeds = await _espressoDatabaseContext
            .RssFeeds
            .AsNoTracking()
            .Include(rssFeed => rssFeed.Category)
            .Include(rssFeed => rssFeed.NewsPortal)
            .Include(rssFeed => rssFeed.RssFeedCategories)
            .ThenInclude(rssFeedCategory => rssFeedCategory.Category)
            .Include(rssFeed => rssFeed.RssFeedContentModifiers)
            .AsSplitQuery()
            .ToArrayAsync();

        var categories = rssFeeds
            .Select(rssFeed => rssFeed.Category!)
            .DistinctBy(category => category.Id)
            .ToArray();

        var articlesDictionary = articles.ToDictionary(article => article.Id);
        _ = _memoryCache.Set(MemoryCacheConstants.ArticleKey, articlesDictionary);
        _ = _memoryCache.Set(MemoryCacheConstants.RssFeedKey, rssFeeds);
        _ = _memoryCache.Set(MemoryCacheConstants.CategoryKey, categories);
    }
}

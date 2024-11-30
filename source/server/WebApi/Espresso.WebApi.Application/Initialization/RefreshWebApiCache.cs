// RefreshWebApiCache.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Diagnostics;
using Espresso.Application.Services.Contracts;
using Espresso.Common.Constants;
using Espresso.Domain.IServices;
using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Espresso.WebApi.Application.Initialization;

public class RefreshWebApiCache : IRefreshWebApiCache
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;
    private readonly IMemoryCache _memoryCache;
    private readonly IArticleLoaderService _articleLoaderService;
    private readonly INewsPortalImagesService _newsPortalImagesService;
    private readonly ILoggerService<IRefreshWebApiCache> _loggerService;

    public RefreshWebApiCache(
        IEspressoDatabaseContext espressoDatabaseContext,
        IMemoryCache memoryCache,
        IArticleLoaderService articleLoaderService,
        INewsPortalImagesService newsPortalImagesService,
        ILoggerService<IRefreshWebApiCache> loggerService)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
        _memoryCache = memoryCache;
        _articleLoaderService = articleLoaderService;
        _newsPortalImagesService = newsPortalImagesService;
        _loggerService = loggerService;
    }

    public async Task RefreshCacheValues()
    {
        var stopwatch = Stopwatch.StartNew();

        await RefreshMemoryCacheValues();
        await _newsPortalImagesService.LoadImagesAndSaveToRootFolder();

        stopwatch.Stop();

        var duration = stopwatch.Elapsed;

        var arguments = new (string, object)[]
        {
            (nameof(duration), duration),
        };

        const string EventName = "Refresh WebApi Cache";
        _loggerService.Log(EventName, LogLevel.Information, arguments);
    }

    private async Task RefreshMemoryCacheValues()
    {
        var regions = await _espressoDatabaseContext
                  .Regions
                  .Include(region => region.NewsPortals)
                  .AsNoTracking()
                  .ToListAsync();

        _ = _memoryCache.Set(
            key: MemoryCacheConstants.RegionKey,
            value: regions.ToList());

        var newsPortals = await _espressoDatabaseContext
            .NewsPortals
            .Include(newsPortal => newsPortal.Category)
            .Include(newsPortal => newsPortal.Region)
            .AsNoTracking()
            .ToListAsync();

        _ = _memoryCache.Set(
            key: MemoryCacheConstants.NewsPortalKey,
            value: newsPortals.ToList());

        var categories = await _espressoDatabaseContext
            .Categories
            .Include(category => category.NewsPortals)
            .AsNoTracking()
            .ToListAsync();

        _ = _memoryCache.Set(
            key: MemoryCacheConstants.CategoryKey,
            value: categories);

        var articles = await _articleLoaderService.LoadArticlesForWebApi(
            newsPortals: newsPortals,
            categories: categories,
            cancellationToken: default);

        _ = _memoryCache.Set(
           key: MemoryCacheConstants.ArticleKey,
           value: articles.ToList());
    }
}

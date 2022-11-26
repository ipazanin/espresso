// WebApiInit.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Diagnostics;
using Espresso.Application.Services.Contracts;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Extensions;
using Espresso.Domain.IServices;
using Espresso.Persistence.Database;
using Espresso.WebApi.Application.HealthChecks;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Espresso.WebApi.Application.Initialization;

/// <summary>
///
/// </summary>
public class WebApiInit : IWebApiInit
{
    private const string ConfigurationFileName = "firebase-key.json";

    private readonly IMemoryCache _memoryCache;
    private readonly IEspressoDatabaseContext _context;
    private readonly IArticleLoaderService _articleLoaderService;
    private readonly ILoggerService<WebApiInit> _loggerService;
    private readonly ReadinessHealthCheck _readinessHealthCheck;

    /// <summary>
    /// Initializes a new instance of the <see cref="WebApiInit"/> class.
    /// </summary>
    /// <param name="memoryCache"></param>
    /// <param name="context"></param>
    /// <param name="articleLoaderService"></param>
    /// <param name="loggerService"></param>
    /// <param name="readinessHealthCheck"></param>
    /// <summary>
    /// Initializes a new instance of the <see cref="WebApiInit"/> class.
    /// </summary>
    public WebApiInit(
        IMemoryCache memoryCache,
        IEspressoDatabaseContext context,
        IArticleLoaderService articleLoaderService,
        ILoggerService<WebApiInit> loggerService,
        ReadinessHealthCheck readinessHealthCheck)
    {
        _memoryCache = memoryCache;
        _context = context;
        _articleLoaderService = articleLoaderService;
        _loggerService = loggerService;
        _readinessHealthCheck = readinessHealthCheck;
    }

    public async Task InitWebApi()
    {
        var stopwatch = Stopwatch.StartNew();

        InitializeGoogleServices();

        await _context.Database.MigrateAsync();

        var regions = await _context
            .Regions
            .Include(region => region.NewsPortals)
            .AsNoTracking()
            .ToListAsync();

        _memoryCache.Set(
            key: MemoryCacheConstants.RegionKey,
            value: regions.ToList());

        var newsPortals = await _context
            .NewsPortals
            .Include(newsPortal => newsPortal.Category)
            .Include(newsPortal => newsPortal.Region)
            .AsNoTracking()
            .ToListAsync();

        _memoryCache.Set(
            key: MemoryCacheConstants.NewsPortalKey,
            value: newsPortals.ToList());

        var categories = await _context
            .Categories
            .Include(category => category.NewsPortals)
            .AsNoTracking()
            .ToListAsync();

        _memoryCache.Set(
            key: MemoryCacheConstants.CategoryKey,
            value: categories);

        var articles = await _articleLoaderService.LoadArticlesForWebApi(
            newsPortals: newsPortals,
            categories: categories,
            cancellationToken: default);

        _memoryCache.Set(
           key: MemoryCacheConstants.ArticleKey,
           value: articles.ToList());

        stopwatch.Stop();

        var eventName = Event.WebApiInit.GetDisplayName();
        var duration = stopwatch.Elapsed;
        var categoriesCount = categories.Count;
        var newsPortalsCount = newsPortals.Count;
        var allArticlesCount = articles.Count();

        var arguments = new (string, object)[]
        {
            (nameof(duration), duration),
            (nameof(categoriesCount), categoriesCount),
            (nameof(newsPortalsCount), newsPortalsCount),
            (nameof(allArticlesCount), allArticlesCount),
        };

        _loggerService.Log(eventName, LogLevel.Information, arguments);

        _readinessHealthCheck.ReadinessTaskCompleted = true;
    }

    private static void InitializeGoogleServices()
    {
        var firebaseKeyPath = Path.Combine(
            path1: AppDomain.CurrentDomain.BaseDirectory ?? string.Empty,
            path2: ConfigurationFileName);
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile(firebaseKeyPath),
        });
    }
}

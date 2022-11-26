// ParseArticlesCronJob.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Diagnostics;
using Cronos;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Extensions;
using Espresso.Dashboard.Application.Articles.Commands.DeleteOldArticles;
using Espresso.Dashboard.Application.HealthChecks;
using Espresso.Dashboard.Configuration;
using Espresso.Dashboard.ParseRssFeeds;
using Espresso.Domain.Entities;
using Espresso.Domain.Infrastructure;
using Espresso.Domain.IServices;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Espresso.Dashboard.CronJobs;

public class ParseArticlesCronJob : CronJob<ParseArticlesCronJob>
{
    private readonly IMemoryCache _memoryCache;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ReadinessHealthCheck _readinessHealthCheck;

    /// <summary>
    /// Initializes a new instance of the <see cref="ParseArticlesCronJob"/> class.
    /// </summary>
    /// <param name="memoryCache"></param>
    /// <param name="cronJobConfiguration"></param>
    /// <param name="serviceScopeFactory"></param>
    /// <param name="readinessHealthCheck"></param>
    public ParseArticlesCronJob(
        IMemoryCache memoryCache,
        ICronJobConfiguration<ParseArticlesCronJob> cronJobConfiguration,
        IServiceScopeFactory serviceScopeFactory,
        ReadinessHealthCheck readinessHealthCheck)
        : base(
            cronJobConfiguration: cronJobConfiguration,
            serviceScopeFactory: serviceScopeFactory)
    {
        _memoryCache = memoryCache;
        _serviceScopeFactory = serviceScopeFactory;
        _readinessHealthCheck = readinessHealthCheck;
    }

    protected override CronExpression CronExpression
    {
        get
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var settingProvider = scope.ServiceProvider.GetRequiredService<ISettingProvider>();

            return CronExpression.Parse(settingProvider.LatestSetting.JobsSetting.ParseArticlesCronExpression);
        }
    }

    private IDictionary<Guid, Article> Articles { get; set; } = new Dictionary<Guid, Article>();

    private IEnumerable<RssFeed> RssFeeds { get; set; } = Array.Empty<RssFeed>();

    private IEnumerable<Category> Categories { get; set; } = Array.Empty<Category>();

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IEspressoDatabaseContext>();
        var loggerService = scope.ServiceProvider.GetRequiredService<ILoggerService<ParseArticlesCronJob>>();

        await context.Database.MigrateAsync(cancellationToken: cancellationToken);

        var newsPortals = await context
            .NewsPortals
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);

        var categories = await context
            .Categories
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);

        var articles = await context.Articles
            .Include(article => article.ArticleCategories)
            .Include(article => article.NewsPortal)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);

        var rssFeeds = await context.RssFeeds
            .Include(rssFeed => rssFeed.Category)
            .Include(rssFeed => rssFeed.NewsPortal)
            .Include(rssFeed => rssFeed.RssFeedCategories)
            .ThenInclude(rssFeedCategory => rssFeedCategory.Category)
            .Include(rssFeed => rssFeed.RssFeedContentModifiers)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync(cancellationToken: cancellationToken);

        RssFeeds = rssFeeds;

        Categories = categories;

        Articles = articles.ToDictionary(article => article.Id);
        _memoryCache.Set(MemoryCacheConstants.ArticleKey, Articles);

        stopwatch.Stop();

        var eventName = Event.DashboardEspressoDatabaseInit.GetDisplayName();
        var duration = stopwatch.Elapsed;
        var categoriesCount = categories.Count;
        var newsPortalsCount = newsPortals.Count;
        var allArticlesCount = articles.Count;
        var rssFeedCount = rssFeeds.Count;

        var arguments = new List<(string parameterName, object parameterValue)>
        {
            (nameof(duration), duration),
            (nameof(categoriesCount), categoriesCount),
            (nameof(newsPortalsCount), newsPortalsCount),
            (nameof(allArticlesCount), allArticlesCount),
            (nameof(rssFeedCount), rssFeedCount),
        };

        loggerService.Log(eventName, LogLevel.Information, arguments);

        _readinessHealthCheck.ReadinessTaskCompleted = true;

        await base.StartAsync(cancellationToken);
    }

    public override async Task DoWork(CancellationToken cancellationToken)
    {
        await CreateArticles(cancellationToken);
        await DeleteArticles(cancellationToken);
        _memoryCache.Set(MemoryCacheConstants.ArticleKey, Articles);

#if DEBUG
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        ;
#endif
    }

    private async Task CreateArticles(CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var configuration = scope.ServiceProvider.GetRequiredService<IDashboardConfiguration>();

        _ = await mediator.Send(
            request: new ParseRssFeedsCommand
            {
                TargetedApiVersion = configuration.AppConfiguration.RssFeedParserMajorMinorVersion,
                ConsumerVersion = configuration.AppConfiguration.Version,
                DeviceType = DeviceType.RssFeedParser,
                Articles = Articles,
                RssFeeds = RssFeeds,
                Categories = Categories,
            },
            cancellationToken: cancellationToken);
    }

    private async Task DeleteArticles(CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var configuration = scope.ServiceProvider.GetRequiredService<IDashboardConfiguration>();

        _ = await mediator.Send(
            request: new DeleteOldArticlesCommand
            {
                Articles = Articles,
                DeviceType = DeviceType.RssFeedParser,
                ConsumerVersion = configuration.AppConfiguration.Version,
                TargetedApiVersion = configuration.AppConfiguration.Version,
            },
            cancellationToken: cancellationToken);
    }
}

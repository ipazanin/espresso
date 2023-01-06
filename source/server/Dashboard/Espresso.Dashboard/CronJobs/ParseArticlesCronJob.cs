// ParseArticlesCronJob.cs
//
// © 2022 Espresso News. All rights reserved.

using Cronos;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Dashboard.Application.Articles.Commands.DeleteOldArticles;
using Espresso.Dashboard.Application.HealthChecks;
using Espresso.Dashboard.Application.IServices;
using Espresso.Dashboard.Application.RssFeeds.Commands.ParseRssFeeds;
using Espresso.Dashboard.Configuration;
using Espresso.Domain.Entities;
using Espresso.Domain.Infrastructure;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.Dashboard.CronJobs;

public class ParseArticlesCronJob : CronJob<ParseArticlesCronJob>
{
    private readonly IMemoryCache _memoryCache;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="ParseArticlesCronJob"/> class.
    /// </summary>
    /// <param name="memoryCache"></param>
    /// <param name="cronJobConfiguration"></param>
    /// <param name="serviceScopeFactory"></param>
    public ParseArticlesCronJob(
        IMemoryCache memoryCache,
        ICronJobConfiguration cronJobConfiguration,
        IServiceScopeFactory serviceScopeFactory)
        : base(
            cronJobConfiguration: cronJobConfiguration,
            serviceScopeFactory: serviceScopeFactory)
    {
        _memoryCache = memoryCache;
        _serviceScopeFactory = serviceScopeFactory;
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

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var refreshMemoryCacheService = scope.ServiceProvider.GetRequiredService<IRefreshDashboardCacheService>();

        await refreshMemoryCacheService.RefreshCache();

        var readinessHealthCheck = scope.ServiceProvider.GetRequiredService<ReadinessHealthCheck>();
        readinessHealthCheck.SetApplicationStateAsReady();

        await base.StartAsync(cancellationToken);
    }

    public override async Task DoWork(CancellationToken cancellationToken)
    {
        await CreateArticles(cancellationToken);
        await DeleteArticles(cancellationToken);
    }

    private async Task CreateArticles(CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var configuration = scope.ServiceProvider.GetRequiredService<IDashboardConfiguration>();

        var articles = _memoryCache.Get<IDictionary<Guid, Article>>(MemoryCacheConstants.ArticleKey)!;
        var rssFeeds = _memoryCache.Get<IEnumerable<RssFeed>>(MemoryCacheConstants.RssFeedKey)!;
        var categories = _memoryCache.Get<IEnumerable<Category>>(MemoryCacheConstants.CategoryKey)!;

        _ = await mediator.Send(
            request: new ParseRssFeedsCommand
            {
                TargetedApiVersion = configuration.AppConfiguration.RssFeedParserMajorMinorVersion,
                ConsumerVersion = configuration.AppConfiguration.Version,
                DeviceType = DeviceType.RssFeedParser,
                Articles = articles,
                RssFeeds = rssFeeds,
                Categories = categories,
            },
            cancellationToken: cancellationToken);
    }

    private async Task DeleteArticles(CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var configuration = scope.ServiceProvider.GetRequiredService<IDashboardConfiguration>();
        var articles = _memoryCache.Get<IDictionary<Guid, Article>>(MemoryCacheConstants.ArticleKey)!;

        _ = await mediator.Send(
            request: new DeleteOldArticlesCommand
            {
                Articles = articles,
                DeviceType = DeviceType.RssFeedParser,
                ConsumerVersion = configuration.AppConfiguration.Version,
                TargetedApiVersion = configuration.AppConfiguration.Version,
            },
            cancellationToken: cancellationToken);
    }
}

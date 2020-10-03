using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.Application.IServices;
using Espresso.WebApi.Application.ApplicationDownloads.Commands.CreateApplicationDownload;
using Espresso.WebApi.Application.ApplicationDownloads.Queries.GetApplicationDownloadStatistics;
using Espresso.WebApi.Application.Articles.Commands.CalculateTrendingScore;
using Espresso.WebApi.Application.Articles.Commands.HideArticle;
using Espresso.WebApi.Application.Articles.Commands.IncrementTrendingArticleScore;
using Espresso.WebApi.Application.Articles.Commands.ToggleFeaturedArticle;
using Espresso.WebApi.Application.Articles.Commands.UpdateInMemoryArticles;
using Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles;
using Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_1_3;
using Espresso.WebApi.Application.Articles.Queries.GetFeaturedArticles;
using Espresso.WebApi.Application.Articles.Queries.GetLatestArticles;
using Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_3;
using Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles;
using Espresso.WebApi.Application.Categories.Queries.GetCategories;
using Espresso.WebApi.Application.Configuration.Queries.GetConfiguration;
using Espresso.WebApi.Application.Configuration.Queries.GetConfiguration_1_3;
using Espresso.WebApi.Application.Configuration.Queries.GetWebConfiguration;
using Espresso.WebApi.Application.NewsPortals.Commands.NewSourcesRequest;
using Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals;
using Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals_1_3;
using Espresso.WebApi.Application.Notifications.Commands.SendArticlesNotifications;
using Espresso.WebApi.Application.Notifications.Commands.SendPushNotification;
using Espresso.WebApi.Application.Notifications.Queries.GetPushNotifications;
using Espresso.WebApi.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Espresso.WebApi.CronJobs
{
    /// <summary>
    /// 
    /// </summary>
    public class WebApiPerformanceCronJob : CronJob<WebApiPerformanceCronJob>
    {
        #region Constants
        private static readonly List<string> s_requestNames = new List<string>
        {
            nameof(CreateApplicationDownloadCommand),
            nameof(GetApplicationDownloadStatisticsQuery),
            nameof(CalculateTrendingScoreCommand),
            nameof(HideArticleCommand),
            nameof(IncrementNumberOfClicksCommand),
            nameof(ToggleFeaturedArticleCommand),
            nameof(UpdateInMemoryArticlesCommand),
            nameof(GetCategoryArticlesQuery),
            nameof(GetCategoryArticlesQuery_1_3),
            nameof(GetFeaturedArticlesQuery),
            nameof(GetLatestArticlesQuery),
            nameof(GetLatestArticlesQuery_1_3),
            nameof(GetTrendingArticlesQuery),
            nameof(GetCategoriesQuery),
            nameof(GetConfigurationQuery),
            nameof(GetConfigurationQuery_1_3),
            nameof(GetWebConfigurationQuery),
            nameof(NewsSourcesRequestCommand),
            nameof(GetNewsPortalsQuery),
            nameof(GetNewsPortalsQuery_1_3),
            nameof(SendArticlesNotificationsCommand),
            nameof(SendPushNotificationCommand),
            nameof(GetPushNotificationsQuery),
        };
        #endregion

        #region Fields
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ICronJobConfiguration<WebApiPerformanceCronJob> _cronJobConfiguration;
        private readonly IWebApiConfiguration _webApiConfiguration;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="cronJobConfiguration"></param>
        /// <param name="webApiConfiguration"></param>
        /// <returns></returns>
        public WebApiPerformanceCronJob(
            IServiceScopeFactory serviceScopeFactory,
            ILoggerFactory loggerFactory,
            ICronJobConfiguration<WebApiPerformanceCronJob> cronJobConfiguration,
            IWebApiConfiguration webApiConfiguration
        ) : base(
            cronJobConfiguration: cronJobConfiguration,
            loggerFactory: loggerFactory,
            serviceScopeFactory: serviceScopeFactory
        )
        {
            _serviceScopeFactory = serviceScopeFactory;
            _cronJobConfiguration = cronJobConfiguration;
            _webApiConfiguration = webApiConfiguration;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await base.StartAsync(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public override async Task DoWork(CancellationToken stoppingToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var slackService = scope.ServiceProvider.GetRequiredService<ISlackService>();
            var memoryCache = scope.ServiceProvider.GetRequiredService<IMemoryCache>();

            var data = new List<(string name, int count, TimeSpan duration)>();


            foreach (var requestName in s_requestNames)
            {
                var requestData = CalculatePerformance(
                    memoryCache: memoryCache,
                    requestName: requestName
                );
                data.Add(requestData);
            }

            await slackService.LogPerformance(
                applicationName: "Web Api",
                data: data,
                appEnvironment: _webApiConfiguration.AppConfiguration.AppEnvironment,
                cancellationToken: stoppingToken
            );
        }

        private static (string name, int count, TimeSpan duration) CalculatePerformance(
            IMemoryCache memoryCache,
            string requestName
        )
        {
            var performanceMeasurementKey = $"{requestName}PerformanceKey";
            var (total, count) = memoryCache.GetOrCreate(
                key: performanceMeasurementKey,
                factory: entry =>
                {
                    var total = new TimeSpan();
                    var count = 0;
                    return (total, count);
                }
            );

            if (count == 0)
            {
                return (requestName, count, total);
            }
            return (requestName, count, total / count);
        }
        #endregion
    }
}

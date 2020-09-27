using System;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.Application.IServices;
using Espresso.ParserDeleter.Application.DeleteOldArticles;
using Espresso.ParserDeleter.Configuration;
using Espresso.ParserDeleter.ParseRssFeeds;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Espresso.ParserDeleter.CronJobs
{
    public class ParserDeleterPerformanceCronJob : CronJob<ParserDeleterPerformanceCronJob>
    {
        #region Fields
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ICronJobConfiguration<ParserDeleterPerformanceCronJob> _cronJobConfiguration;
        private readonly IParserDeleterConfiguration _parserDeleterConfiguration;
        #endregion

        #region Constructors
        public ParserDeleterPerformanceCronJob(
            IServiceScopeFactory serviceScopeFactory,
            ILoggerFactory loggerFactory,
            ICronJobConfiguration<ParserDeleterPerformanceCronJob> cronJobConfiguration,
            IParserDeleterConfiguration parserDeleterConfiguration
        ) : base(
            cronJobConfiguration: cronJobConfiguration,
            loggerFactory: loggerFactory,
            serviceScopeFactory: serviceScopeFactory
        )
        {
            _serviceScopeFactory = serviceScopeFactory;
            _cronJobConfiguration = cronJobConfiguration;
            _parserDeleterConfiguration = parserDeleterConfiguration;
        }
        #endregion

        #region Methods
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            // await Task.Delay(
            //     delay: TimeSpan.FromSeconds(60),
            //     cancellationToken: cancellationToken
            // );

            await base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken stoppingToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var slackService = scope.ServiceProvider.GetRequiredService<ISlackService>();
            var memoryCache = scope.ServiceProvider.GetRequiredService<IMemoryCache>();

            var parseRssFeedsPerformance = CalculateAveragePerformance(
                memoryCache: memoryCache,
                requestName: nameof(ParseRssFeedsCommand)
            );
            var deleteOldArticlesPerformance = CalculateAveragePerformance(
                memoryCache: memoryCache,
                requestName: nameof(DeleteOldArticlesCommand)
            );

            await slackService.LogParserDeleterPerformance(
                parseRssFeedsPerformance: parseRssFeedsPerformance,
                deleteOldArticlesPerformance: deleteOldArticlesPerformance,
                appEnvironment: _parserDeleterConfiguration.AppConfiguration.AppEnvironment,
                cancellationToken: stoppingToken
            );
        }

        private static TimeSpan CalculateAveragePerformance(
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
                return total;
            }

            return total / count;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.Application.IServices;
using Espresso.Domain.IServices;
using Espresso.ParserDeleter.Application.DeleteOldArticles;
using Espresso.ParserDeleter.Configuration;
using Espresso.ParserDeleter.ParseRssFeeds;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Espresso.ParserDeleter.CronJobs
{
    public class ParserDeleterReportCronJob : CronJob<ParserDeleterReportCronJob>
    {
        #region Constants
        private static readonly List<string> s_requestNames = new List<string>
        {
            nameof(ParseRssFeedsCommand),
            nameof(DeleteOldArticlesCommand),
        };
        #endregion

        #region Fields
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ICronJobConfiguration<ParserDeleterReportCronJob> _cronJobConfiguration;
        private readonly IParserDeleterConfiguration _parserDeleterConfiguration;
        #endregion

        #region Constructors
        public ParserDeleterReportCronJob(
            IServiceScopeFactory serviceScopeFactory,
            ICronJobConfiguration<ParserDeleterReportCronJob> cronJobConfiguration,
            IParserDeleterConfiguration parserDeleterConfiguration,
            ILoggerService<CronJob<ParserDeleterReportCronJob>> loggerService
        ) : base(
            cronJobConfiguration: cronJobConfiguration,
            loggerService: loggerService,
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
            await base.StartAsync(cancellationToken);
        }

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
                applicationName: "Parser Deleter",
                data: data,
                appEnvironment: _parserDeleterConfiguration.AppConfiguration.AppEnvironment,
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

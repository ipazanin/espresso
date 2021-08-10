// AnalyticsCronJob.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.Application.Services.Contracts;
using Espresso.Common.Enums;
using Espresso.Domain.Entities;
using Espresso.Domain.Utilities;
using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.WebApi.Jobs.CronJobs
{
    /// <summary>
    ///
    /// </summary>
    public class AnalyticsCronJob : CronJob<AnalyticsCronJob>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsCronJob"/> class.
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        /// <param name="cronJobConfiguration"></param>
        public AnalyticsCronJob(
            IServiceScopeFactory serviceScopeFactory,
            ICronJobConfiguration<AnalyticsCronJob> cronJobConfiguration
        )
            : base(
            cronJobConfiguration: cronJobConfiguration,
            serviceScopeFactory: serviceScopeFactory
        )
        {
            _scopeFactory = serviceScopeFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cancellationToken"></param>
        public override async Task DoWork(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var espressoDatabaseContext = serviceProvider.GetRequiredService<IEspressoDatabaseContext>();
            var slackService = serviceProvider.GetRequiredService<ISlackService>();
            var googleAnalyticsService = serviceProvider.GetRequiredService<IGoogleAnalyticsService>();

            var applicationDownloads = await espressoDatabaseContext.ApplicationDownload.ToListAsync(cancellationToken);
            var (todayAndroidCount, todayIosCount, totalAndroidCount, totalIosCount) = CalculateAppDownloadsPerDeviceType(applicationDownloads);

            var activeUsers = await googleAnalyticsService.GetNumberOfActiveUsersFromYesterday();
            var revenue = await googleAnalyticsService.GetTotalRevenueFromYesterday();

            await slackService.LogAppDownloadStatistics(
                    yesterdayAndroidCount: todayAndroidCount,
                    yesterdayIosCount: todayIosCount,
                    totalAndroidCount: totalAndroidCount,
                    totalIosCount: totalIosCount,
                    activeUsers: activeUsers,
                    revenue: revenue,
                    cancellationToken: cancellationToken
            );
        }

        private static (int todayAndroidCount, int todayIosCount, int totalAndroidCount, int totalIosCount) CalculateAppDownloadsPerDeviceType(
            IEnumerable<ApplicationDownload> applicationDownloads
        )
        {
            var todayAndroidCount = applicationDownloads.Count(applicationDownloads =>
                applicationDownloads.MobileDeviceType == DeviceType.Android &&
                applicationDownloads.DownloadedTime.Date == DateTimeUtility.YesterdaysDate
            );

            var todayIosCount = applicationDownloads.Count(applicationDownloads =>
                applicationDownloads.MobileDeviceType == DeviceType.Ios &&
                applicationDownloads.DownloadedTime.Date == DateTimeUtility.YesterdaysDate
            );

            var totalIosCount = applicationDownloads.Count(applicationDownloads =>
                applicationDownloads.MobileDeviceType == DeviceType.Ios &&
                applicationDownloads.DownloadedTime.Date <= DateTimeUtility.YesterdaysDate
            );
            var totalAndroidCount = applicationDownloads.Count(applicationDownloads =>
                applicationDownloads.MobileDeviceType == DeviceType.Android &&
                applicationDownloads.DownloadedTime.Date <= DateTimeUtility.YesterdaysDate
            );

            return (todayAndroidCount, todayIosCount, totalAndroidCount, totalIosCount);
        }
    }
}

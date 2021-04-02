using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Espresso.Common.Enums;
using Espresso.Persistence.IRepositories;
using Espresso.Application.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.Domain.Utilities;
using System.Collections;
using System.Collections.Generic;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Jobs.CronJobs
{
    /// <summary>
    /// 
    /// </summary>
    public class AnalyticsCronJob : CronJob<AnalyticsCronJob>
    {

        #region Fields

        private readonly IServiceScopeFactory _scopeFactory;

        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        /// <param name="cronJobConfiguration"></param>
        /// <returns></returns>
        public AnalyticsCronJob(
            IServiceScopeFactory serviceScopeFactory,
            ICronJobConfiguration<AnalyticsCronJob> cronJobConfiguration
        ) : base(
            cronJobConfiguration: cronJobConfiguration,
            serviceScopeFactory: serviceScopeFactory
        )
        {
            _scopeFactory = serviceScopeFactory;
        }
        #endregion

        #region  Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task DoWork(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var applicationDownloadRepository = serviceProvider.GetRequiredService<IApplicationDownloadRepository>();
            var slackService = serviceProvider.GetRequiredService<ISlackService>();
            var googleAnalyticsService = serviceProvider.GetRequiredService<IGoogleAnalyticsService>();

            var applicationDownloads = await applicationDownloadRepository.GetApplicationDownloads();
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
        #endregion
    }
}

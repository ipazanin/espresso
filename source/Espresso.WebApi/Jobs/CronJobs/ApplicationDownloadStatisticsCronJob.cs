using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Espresso.Common.Enums;
using Espresso.Persistence.IRepositories;
using Espresso.Application.IServices;
using Microsoft.Extensions.DependencyInjection;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.WebApi.Configuration;
using Espresso.Domain.Utilities;

namespace Espresso.WebApi.Jobs.CronJobs
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationDownloadStatisticsCronJob : CronJob<ApplicationDownloadStatisticsCronJob>
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
        public ApplicationDownloadStatisticsCronJob(
            IServiceScopeFactory serviceScopeFactory,
            ICronJobConfiguration<ApplicationDownloadStatisticsCronJob> cronJobConfiguration
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

            var applicationDownloads = await applicationDownloadRepository.GetApplicationDownloads();

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

            await slackService.LogAppDownloadStatistics(
                    yesterdayAndroidCount: todayAndroidCount,
                    yesterdayIosCount: todayIosCount,
                    totalAndroidCount: totalAndroidCount,
                    totalIosCount: totalIosCount,
                    cancellationToken: cancellationToken
            );
        }
        #endregion
    }
}

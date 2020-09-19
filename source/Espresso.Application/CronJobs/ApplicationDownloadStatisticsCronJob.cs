using System;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using System.Threading.Tasks;
using System.Threading;
using Espresso.Domain.IServices;
using System.Linq;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Common.Enums;
using Espresso.Persistence.IRepositories;
using Microsoft.Extensions.Logging;

namespace Espresso.Application.CronJobs
{
    public class ApplicationDownloadStatisticsCronJob : CronJob<ApplicationDownloadStatisticsCronJob>
    {
        #region Fields
        private readonly ISlackService _slackService;
        private readonly IApplicationDownloadRepository _applicationDownloadRepository;
        private readonly AppEnvironment _appEnvironment;
        #endregion

        #region Constructors
        public ApplicationDownloadStatisticsCronJob(
            ICronJobConfiguration<ApplicationDownloadStatisticsCronJob> cronJobConfiguration,
            ISlackService slackService,
            IApplicationDownloadRepository applicationDownloadRepository,
            ILoggerFactory loggerFactory
        ) : base(cronJobConfiguration, loggerFactory)
        {
            _slackService = slackService;
            _applicationDownloadRepository = applicationDownloadRepository;
            _appEnvironment = cronJobConfiguration.AppEnvironment;
        }
        #endregion

        #region  Methods
        public override async Task DoWork(CancellationToken cancellationToken)
        {
            var applicationDownloads = await _applicationDownloadRepository.GetApplicationDownloads();

            var yesterday = DateTime.UtcNow.AddDays(-1);

            var todayAndroidCount = applicationDownloads.Count(applicationDownloads =>
                applicationDownloads.MobileDeviceType == DeviceType.Android &&
                applicationDownloads.DownloadedTime.Date == yesterday.Date
            );

            var todayIosCount = applicationDownloads.Count(applicationDownloads =>
                applicationDownloads.MobileDeviceType == DeviceType.Ios &&
                applicationDownloads.DownloadedTime.Date == yesterday.Date
            );

            var totalIosCount = applicationDownloads.Count(applicationDownloads =>
                applicationDownloads.MobileDeviceType == DeviceType.Ios
            );
            var totalAndroidCount = applicationDownloads.Count(applicationDownloads =>
                applicationDownloads.MobileDeviceType == DeviceType.Android
            );

            await _slackService.LogAppDownloadStatistics(
                    yesterdayAndroidCount: todayAndroidCount,
                    yesterdayIosCount: todayIosCount,
                    totalAndroidCount: totalAndroidCount,
                    totalIosCount: totalIosCount,
                    appEnvironment: _appEnvironment,
                    cancellationToken: cancellationToken
            );
        }
        #endregion
    }
}

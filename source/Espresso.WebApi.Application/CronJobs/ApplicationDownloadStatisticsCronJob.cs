using System;
using Espresso.WebApi.Application.Infrastructure.CronJobsInfrastructure;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Persistence.IRepositories;
using Microsoft.Extensions.Logging;
using Espresso.WebApi.Application.IServices;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.WebApi.Application.CronJobs
{
    public class ApplicationDownloadStatisticsCronJob : CronJob<ApplicationDownloadStatisticsCronJob>
    {
        #region Fields
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ICronJobConfiguration<ApplicationDownloadStatisticsCronJob> _cronJobConfiguration;
        #endregion

        #region Constructors
        public ApplicationDownloadStatisticsCronJob(
            IServiceScopeFactory scopeFactory,
            ICronJobConfiguration<ApplicationDownloadStatisticsCronJob> cronJobConfiguration,
            ILoggerFactory loggerFactory
        ) : base(cronJobConfiguration, loggerFactory)
        {
            _scopeFactory = scopeFactory;
            _cronJobConfiguration = cronJobConfiguration;
        }
        #endregion

        #region  Methods
        public override async Task DoWork(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var applicationDownloadRepository = serviceProvider.GetRequiredService<IApplicationDownloadRepository>();
            var slackService = serviceProvider.GetRequiredService<ISlackService>();

            var applicationDownloads = await applicationDownloadRepository.GetApplicationDownloads();

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

            await slackService.LogAppDownloadStatistics(
                    yesterdayAndroidCount: todayAndroidCount,
                    yesterdayIosCount: todayIosCount,
                    totalAndroidCount: totalAndroidCount,
                    totalIosCount: totalIosCount,
                    appEnvironment: _cronJobConfiguration.AppEnvironment,
                    cancellationToken: cancellationToken
            );
        }
        #endregion
    }
}

using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Persistence.IRepositories;
using Microsoft.Extensions.Logging;
using Espresso.Application.IServices;
using Microsoft.Extensions.DependencyInjection;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.WebApi.Configuration;

namespace Espresso.WebApi.CronJobs
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationDownloadStatisticsCronJob : CronJob<ApplicationDownloadStatisticsCronJob>
    {
        #region Fields
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IWebApiConfiguration _webApiConfiguration;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        /// <param name="cronJobConfiguration"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="webApiConfiguration"></param>
        /// <returns></returns>
        public ApplicationDownloadStatisticsCronJob(
            IServiceScopeFactory serviceScopeFactory,
            ICronJobConfiguration<ApplicationDownloadStatisticsCronJob> cronJobConfiguration,
            ILoggerFactory loggerFactory,
            IWebApiConfiguration webApiConfiguration
        ) : base(
            cronJobConfiguration: cronJobConfiguration,
            loggerFactory: loggerFactory,
            serviceScopeFactory: serviceScopeFactory
        )
        {
            _scopeFactory = serviceScopeFactory;
            _webApiConfiguration = webApiConfiguration;
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
                    appEnvironment: _webApiConfiguration.AppConfiguration.AppEnvironment,
                    cancellationToken: cancellationToken
            );
        }
        #endregion
    }
}

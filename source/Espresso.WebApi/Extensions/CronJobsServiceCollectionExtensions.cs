using System;
using Microsoft.Extensions.DependencyInjection;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.CronJobs;

namespace Espresso.WebApi.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class CronJobsServiceCollectionExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="webApiConfiguration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCronJobs(
            this IServiceCollection services,
            IWebApiConfiguration webApiConfiguration
        )
        {
            services.AddCronJob<ApplicationDownloadStatisticsCronJob>(cronJobConfiguration =>
            {
                cronJobConfiguration.CronExpression = webApiConfiguration
                    .CronJobsConfiguration
                    .ApplicationDownloadStatisticsCronExpression;
                cronJobConfiguration.TimeZoneInfo = TimeZoneInfo.Utc;
                cronJobConfiguration.Version = webApiConfiguration.AppConfiguration.Version;
                cronJobConfiguration.AppEnvironment = webApiConfiguration.AppConfiguration.AppEnvironment;
            });

            return services;
        }
    }
}

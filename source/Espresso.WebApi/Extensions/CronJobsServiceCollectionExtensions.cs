using System;
using Espresso.Application.CronJobs;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.Common.Enums;
using Microsoft.Extensions.DependencyInjection;

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
        /// <param name="appEnvironment"></param>
        /// <returns></returns>
        public static IServiceCollection AddCronJobs(this IServiceCollection services, AppEnvironment appEnvironment)
        {
            services.AddCronJob<ApplicationDownloadStatisticsCronJob>(cronJobConfiguration =>
            {
                cronJobConfiguration.AppEnvironment = appEnvironment;
                cronJobConfiguration.CronExpression = "0 10 * * *";
                cronJobConfiguration.TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Europe/Zagreb");
            });

            return services;
        }

        private static IServiceCollection AddCronJob<T>(
            this IServiceCollection services,
            Action<ICronJobConfiguration<T>> options
        ) where T : CronJob<T>
        {
            if (options == null)
            {
                throw new ArgumentNullException(
                    paramName: nameof(options),
                    message: "Please provide Schedule Configurations."
                );
            }

            var config = new CronJobConfiguration<T>();
            options.Invoke(config);

            if (string.IsNullOrWhiteSpace(config.CronExpression))
            {
                throw new ArgumentNullException(
                    paramName: nameof(CronJobConfiguration<T>.CronExpression),
                    message: "Empty Cron Expression is not allowed."
                );
            }

            if (config.AppEnvironment == AppEnvironment.Undefined)
            {
#pragma warning disable CA2208
                throw new ArgumentException(
                    message: "Empty AppEnvironment is not allowed.",
                    paramName: nameof(CronJobConfiguration<T>.AppEnvironment)
                );
#pragma warning restore CA2208
            }

            services.AddSingleton<ICronJobConfiguration<T>>(config);
            services.AddHostedService<T>();

            return services;
        }
    }
}

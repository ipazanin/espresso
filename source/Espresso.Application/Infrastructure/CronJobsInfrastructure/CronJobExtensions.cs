using System;
using Microsoft.Extensions.DependencyInjection;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;

namespace Espresso.WebApi.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class CronJobExtensions
    {
        public static IServiceCollection AddCronJob<T>(
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

            if (string.IsNullOrWhiteSpace(config.Version))
            {
                throw new ArgumentNullException(
                    paramName: nameof(CronJobConfiguration<T>.Version),
                    message: "Empty version is not allowed."
                );
            }

            if (config.AppEnvironment == default)
            {
                throw new ArgumentNullException(
                    paramName: nameof(CronJobConfiguration<T>.AppEnvironment),
                    message: "Empty App environment is not allowed."
                );
            }

            services.AddSingleton<ICronJobConfiguration<T>>(config);
            services.AddHostedService<T>();

            return services;
        }
    }
}

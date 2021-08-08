// CronJobExtensions.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.Application.Infrastructure.CronJobsInfrastructure
{
    /// <summary>
    /// Cron Jobs Extensions.
    /// </summary>
    public static class CronJobExtensions
    {
        /// <summary>
        /// Adds cron job to <paramref name="services"/>.
        /// </summary>
        /// <typeparam name="T">Cron job.</typeparam>
        /// <param name="services">Services.</param>
        /// <param name="options">Builds cron job options.</param>
        /// <returns>A reference to this instance after operation is complete.</returns>
        public static IServiceCollection AddCronJob<T>(
            this IServiceCollection services,
            Action<ICronJobConfiguration<T>> options
        )
            where T : CronJob<T>
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
#pragma warning disable S3928 // The parameter name 'CronExpression' is not declared in the argument list.
                throw new ArgumentNullException(
                    paramName: nameof(CronJobConfiguration<T>.CronExpression),
                    message: "Empty Cron Expression is not allowed."
                );
#pragma warning restore S3928
            }

            if (string.IsNullOrWhiteSpace(config.Version))
            {
#pragma warning disable S3928 // The parameter name 'Version' is not declared in the argument list.
                throw new ArgumentNullException(
                    paramName: nameof(CronJobConfiguration<T>.Version),
                    message: "Empty version is not allowed."
                );
#pragma warning restore S3928
            }

            if (config.AppEnvironment == default)
            {
#pragma warning disable S3928 // The parameter name 'AppEnvironment' is not declared in the argument list.
                throw new ArgumentNullException(
                    paramName: nameof(CronJobConfiguration<T>.AppEnvironment),
                    message: "Empty App environment is not allowed."
                );
#pragma warning restore S3928
            }

            services.AddSingleton<ICronJobConfiguration<T>>(config);
            services.AddHostedService<T>();

            return services;
        }
    }
}

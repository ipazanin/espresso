// CronJobConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using Espresso.Common.Enums;

namespace Espresso.Application.Infrastructure.CronJobsInfrastructure
{
    /// <summary>
    /// Represents <see cref="CronJob{T}"/> configuration.
    /// </summary>
    /// <typeparam name="T">Cron job.</typeparam>
    public class CronJobConfiguration<T> : ICronJobConfiguration<T>
        where T : CronJob<T>
    {
        /// <summary>
        /// Gets or sets cron expression.
        /// </summary>
        public string? CronExpression { get; set; }

        /// <summary>
        /// Gets or sets time zone information.
        /// </summary>
        public TimeZoneInfo? TimeZoneInfo { get; set; }

        /// <summary>
        /// Gets or sets application version.
        /// </summary>
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets application environment.
        /// </summary>
        public AppEnvironment AppEnvironment { get; set; }
    }
}

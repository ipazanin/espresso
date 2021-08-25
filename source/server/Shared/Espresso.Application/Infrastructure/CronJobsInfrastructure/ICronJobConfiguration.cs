// ICronJobConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Enums;
using System;

namespace Espresso.Application.Infrastructure.CronJobsInfrastructure
{
    /// <summary>
    /// Cron Job Configuration Contract.
    /// </summary>
    /// <typeparam name="T"><see cref="CronJob{T}"/>.</typeparam>
    public interface ICronJobConfiguration<T>
    {
        /// <summary>
        /// Gets or sets cron Expression.
        /// </summary>
        public string? CronExpression { get; set; }

        /// <summary>
        /// Gets or sets time Zone Information.
        /// </summary>
        public TimeZoneInfo? TimeZoneInfo { get; set; }

        /// <summary>
        /// Gets or sets application Version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets application Environment.
        /// </summary>
        public AppEnvironment AppEnvironment { get; set; }
    }
}

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
        where T : CronJob<T>
    {
        /// <summary>
        /// Gets or sets time Zone Information.
        /// </summary>
        public TimeZoneInfo? TimeZoneInfo { get; }

        /// <summary>
        /// Gets or sets application Version.
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// Gets or sets application Environment.
        /// </summary>
        public AppEnvironment AppEnvironment { get; }
    }
}

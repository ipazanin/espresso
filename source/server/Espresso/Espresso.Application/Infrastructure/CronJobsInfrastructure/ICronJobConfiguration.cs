using System;
using Espresso.Common.Enums;

namespace Espresso.Application.Infrastructure.CronJobsInfrastructure
{
    /// <summary>
    /// Cron Job Configuration Contract.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICronJobConfiguration<T>
    {
        /// <summary>
        /// Cron Expression.
        /// </summary>
        /// <value></value>
        string? CronExpression { get; set; }

        /// <summary>
        /// Time Zone Information.
        /// </summary>
        /// <value></value>
        TimeZoneInfo? TimeZoneInfo { get; set; }

        /// <summary>
        /// Application Version.
        /// </summary>
        /// <value></value>
        string Version { get; set; }

        /// <summary>
        /// Application Environment.
        /// </summary>
        /// <value></value>
        AppEnvironment AppEnvironment { get; set; }
    }
}

using System;
using Espresso.Common.Enums;

namespace Espresso.Application.Infrastructure.CronJobsInfrastructure
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICronJobConfiguration<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        string? CronExpression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        TimeZoneInfo? TimeZoneInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        string Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        AppEnvironment AppEnvironment { get; set; }

    }
}

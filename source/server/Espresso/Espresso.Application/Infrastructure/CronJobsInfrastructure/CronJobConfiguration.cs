using System;
using Espresso.Common.Enums;

namespace Espresso.Application.Infrastructure.CronJobsInfrastructure
{
    public class CronJobConfiguration<T> : ICronJobConfiguration<T>
    {
        public string? CronExpression { get; set; }

        public TimeZoneInfo? TimeZoneInfo { get; set; }

        public string Version { get; set; } = string.Empty;

        public AppEnvironment AppEnvironment { get; set; }
    }
}

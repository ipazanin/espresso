using System;
using Espresso.Common.Enums;

namespace Espresso.Application.Infrastructure.CronJobsInfrastructure
{
    public class CronJobConfiguration<T> : ICronJobConfiguration<T>
    {
        public string? CronExpression { get; set; }

        public TimeZoneInfo? TimeZoneInfo { get; set; }

        public AppEnvironment AppEnvironment { get; set; }
    }
}

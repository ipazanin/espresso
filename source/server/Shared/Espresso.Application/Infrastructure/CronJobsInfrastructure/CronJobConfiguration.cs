// CronJobConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Enums;

namespace Espresso.Application.Infrastructure.CronJobsInfrastructure;

/// <summary>
/// Represents <see cref="CronJob{T}"/> configuration.
/// </summary>
public class CronJobConfiguration : ICronJobConfiguration
{
    public CronJobConfiguration(TimeZoneInfo? timeZoneInfo, string version, AppEnvironment appEnvironment)
    {
        TimeZoneInfo = timeZoneInfo;
        Version = version;
        AppEnvironment = appEnvironment;
    }

    /// <summary>
    /// Gets or sets time zone information.
    /// </summary>
    public TimeZoneInfo? TimeZoneInfo { get; }

    /// <summary>
    /// Gets or sets application version.
    /// </summary>
    public string Version { get; }

    /// <summary>
    /// Gets or sets application environment.
    /// </summary>
    public AppEnvironment AppEnvironment { get; }
}

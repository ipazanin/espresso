// ICronJobConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Enums;

namespace Espresso.Application.Infrastructure.CronJobsInfrastructure;

/// <summary>
/// Cron Job Configuration Contract.
/// </summary>
public interface ICronJobConfiguration
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

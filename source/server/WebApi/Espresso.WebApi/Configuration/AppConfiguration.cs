// AppConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi.Configuration;

/// <summary>
///
/// </summary>
public class AppConfiguration
{
    private readonly IConfigurationSection _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppConfiguration"/> class.
    /// </summary>
    /// <param name="configuration"></param>
    public AppConfiguration(IConfigurationSection configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    ///
    /// </summary>
    public AppEnvironment AppEnvironment => _configuration.GetValue<AppEnvironment>("Environment");

    /// <summary>
    ///
    /// </summary>
    public string Version => _configuration.GetValue<string>("Version")!;

    /// <summary>
    ///
    /// </summary>
    public ApiVersion ApiVersion => new(
        majorVersion: _configuration.GetValue<int>("MajorVersion"),
        minorVersion: _configuration.GetValue<int>("MinorVersion"));

    /// <summary>
    /// Gets all Api Versions.
    /// </summary>
    public IEnumerable<ApiVersion> ApiVersions =>
    [
        ApiVersion,
        new ApiVersion(2, 1),
        new ApiVersion(2, 0),
        new ApiVersion(1, 4),
        new ApiVersion(1, 3),
        new ApiVersion(1, 2),
    ];

    public string AnalyticsSlackWebHook => _configuration.GetValue<string>("AnalyticsSlackWebHook")!;

    public string CrashReportSlackWebHook => _configuration.GetValue<string>("CrashReportSlackWebHook")!;

    public string NewSourceRequestSlackWebHook => _configuration.GetValue<string>("NewSourceRequestSlackWebHook")!;
}

// AppConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Enums;
using Microsoft.Extensions.Configuration;

namespace Espresso.Dashboard.Configuration;

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

    public string MajorMinorVersion => $"{_configuration.GetValue<int>("MajorVersion")}.{_configuration.GetValue<int>("MinorVersion")}";

    public string ServerUrl => _configuration.GetValue<string>("ServerUrl")!;

    public AppEnvironment AppEnvironment => _configuration.GetValue<AppEnvironment>("Environment");

    public string Version => _configuration.GetValue<string>("Version")!;

    public string SlackWebHook => _configuration.GetValue<string>("SlackWebHook")!;

    public string AdminUserPassword => _configuration.GetValue<string>("AdminUserPassword")!;

    public string SendGridApiKey => _configuration.GetValue<string>(nameof(SendGridApiKey))!;
}

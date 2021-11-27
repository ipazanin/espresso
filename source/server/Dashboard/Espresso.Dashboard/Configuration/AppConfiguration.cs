// AppConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Enums;
using Microsoft.Extensions.Configuration;

namespace Espresso.Dashboard.Configuration;

public class AppConfiguration
{
    private readonly IConfigurationSection _configuration;

    public string RssFeedParserMajorMinorVersion => $"{_configuration.GetValue<int>("MajorVersion")}.{_configuration.GetValue<int>("MinorVersion")}";

    public string ServerUrl => _configuration.GetValue<string>("ServerUrl");

    public AppEnvironment AppEnvironment => _configuration.GetValue<AppEnvironment>("Environment");

    public string Version => _configuration.GetValue<string>("Version");

    public string SlackWebHook => _configuration.GetValue<string>("SlackWebHook");

    public string AdminUserPassword => _configuration.GetValue<string>("AdminUserPassword");

    public string SendGridApiKey => _configuration.GetValue<string>(nameof(SendGridApiKey));

    /// <summary>
    /// Initializes a new instance of the <see cref="AppConfiguration"/> class.
    /// </summary>
    /// <param name="configuration"></param>
#pragma warning disable SA1201 // Elements should appear in the correct order
    public AppConfiguration(IConfigurationSection configuration)
#pragma warning restore SA1201 // Elements should appear in the correct order
    {
        _configuration = configuration;
    }
}

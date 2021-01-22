using System;
using System.Diagnostics;
using Espresso.Common.Enums;
using Microsoft.Extensions.Configuration;

namespace Espresso.Dashboard.Configuration
{
    public class AppConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configuration;
        #endregion

        #region Properties
        public string RssFeedParserMajorMinorVersion => $"{_configuration.GetValue<int>("MajorVersion")}.{_configuration.GetValue<int>("MinorVersion")}";

        public string ServerUrl => _configuration.GetValue<string>("ServerUrl");

        public AppEnvironment AppEnvironment => _configuration.GetValue<AppEnvironment>("Environment");

        public string Version => _configuration.GetValue<string>("Version");

        public TimeSpan MaxAgeOfArticles =>
            TimeSpan.FromDays(
                value: _configuration.GetValue<int>("MaxAgeOfArticlesInDays")
            );

        public TimeSpan Uptime => DateTime.UtcNow - Process.GetCurrentProcess().StartTime.ToUniversalTime();

        public string SlackWebHook => _configuration.GetValue<string>("SlackWebHook");

        public int MaxHttpHandlerRetries => _configuration.GetValue<int>("MaxHttpHandlerRetries");
        #endregion

        #region Constructors
        public AppConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}

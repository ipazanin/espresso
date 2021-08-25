// AppConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Espresso.WebApi.Configuration
{
    /// <summary>
    ///
    /// </summary>
    public class AppConfiguration
    {
        private readonly IConfigurationSection _configuration;

        /// <summary>
        ///
        /// </summary>
        public AppEnvironment AppEnvironment => _configuration.GetValue<AppEnvironment>("Environment");

        /// <summary>
        ///
        /// </summary>
        /// <value></value>
        public int NewNewsPortalsPosition => _configuration.GetValue<int>("NewNewsPortalsPosition");

        /// <summary>
        ///
        /// </summary>
        public string Version => _configuration.GetValue<string>("Version");

        /// <summary>
        ///
        /// </summary>
        public ApiVersion ApiVersion => new(
            majorVersion: _configuration.GetValue<int>("MajorVersion"),
            minorVersion: _configuration.GetValue<int>("MinorVersion")
        );

        /// <summary>
        /// Gets all Api Versions.
        /// </summary>
        /// <value></value>
        public IEnumerable<ApiVersion> ApiVersions => new[]
        {
            ApiVersion,
            new ApiVersion(2, 0),
            new ApiVersion(1, 4),
            new ApiVersion(1, 3),
            new ApiVersion(1, 2),
        };

        /// <summary>
        ///
        /// </summary>
        public TimeSpan Uptime => DateTime.UtcNow - Process.GetCurrentProcess().StartTime.ToUniversalTime();

        /// <summary>
        ///
        /// </summary>
        public int FeaturedArticlesTake => _configuration.GetValue<int>("FeaturedArticlesTake");

        /// <summary>
        ///
        /// </summary>
        public string SlackWebHook => _configuration.GetValue<string>("SlackWebHook");

        /// <summary>
        ///
        /// </summary>
        /// <value></value>
        public int MaxHttpHandlerRetries => _configuration.GetValue<int>("MaxHttpHandlerRetries");

        /// <summary>
        /// Initializes a new instance of the <see cref="AppConfiguration"/> class.
        /// </summary>
        /// <param name="configuration"></param>
        public AppConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
    }
}

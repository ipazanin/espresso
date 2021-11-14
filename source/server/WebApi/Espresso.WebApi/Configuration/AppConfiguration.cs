// AppConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

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
        public string Version => _configuration.GetValue<string>("Version");

        /// <summary>
        ///
        /// </summary>
        public ApiVersion ApiVersion => new(
            majorVersion: _configuration.GetValue<int>("MajorVersion"),
            minorVersion: _configuration.GetValue<int>("MinorVersion"));

        /// <summary>
        /// Gets all Api Versions.
        /// </summary>
        public IEnumerable<ApiVersion> ApiVersions => new[]
        {
            ApiVersion,
            new ApiVersion(2, 1),
            new ApiVersion(2, 0),
            new ApiVersion(1, 4),
            new ApiVersion(1, 3),
            new ApiVersion(1, 2),
        };

        /// <summary>
        ///
        /// </summary>
        public string SlackWebHook => _configuration.GetValue<string>("SlackWebHook");

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
}

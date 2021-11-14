// HttpClientConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.Extensions.Configuration;
using System;

namespace Espresso.Dashboard.Configuration
{
    /// <summary>
    /// HttpClientConfiguration.
    /// </summary>
    public class HttpClientConfiguration
    {
        private readonly IConfigurationSection _configurationSection;

        /// <summary>
        /// Gets maximum Number Of Retries.
        /// </summary>
        public int MaxRetries => _configurationSection.GetValue<int>("MaxRetries");

        /// <summary>
        /// Gets hTTP Client Timeout.
        /// </summary>
        public TimeSpan Timeout => TimeSpan.FromSeconds(
            _configurationSection.GetValue<int>("TimeoutInSeconds"));

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientConfiguration"/> class.
        /// </summary>
        /// <param name="configurationSection">Part of app configuration.</param>
#pragma warning disable SA1201 // Elements should appear in the correct order
        public HttpClientConfiguration(
#pragma warning restore SA1201 // Elements should appear in the correct order
            IConfigurationSection configurationSection)
        {
            _configurationSection = configurationSection;
        }
    }
}

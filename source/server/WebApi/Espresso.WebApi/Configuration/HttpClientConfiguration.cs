// HttpClientConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi.Configuration
{
    /// <summary>
    /// HttpClientConfiguration.
    /// </summary>
    public class HttpClientConfiguration
    {
        private readonly IConfigurationSection _configurationSection;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientConfiguration"/> class.
        /// HttpClientConfiguration Constructor.
        /// </summary>
        public HttpClientConfiguration(IConfigurationSection configurationSection)
        {
            _configurationSection = configurationSection;
        }

        /// <summary>
        /// 
        /// </summary>
        public int MaxRetries => _configurationSection.GetValue<int>("MaxRetries");

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan Timeout => TimeSpan.FromSeconds(
            _configurationSection.GetValue<int>("TimeoutInSeconds"));
    }
}

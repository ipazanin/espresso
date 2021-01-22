using System;
using Microsoft.Extensions.Configuration;

namespace Espresso.Dashboard.Configuration
{
    /// <summary>
    /// HttpClientConfiguration
    /// </summary>
    public class HttpClientConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configurationSection;
        #endregion Fields

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int MaxRetries => _configurationSection.GetValue<int>("MaxRetries");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TimeSpan Timeout => TimeSpan.FromSeconds(
            _configurationSection.GetValue<int>("TimeoutInSeconds")
        );
        #endregion Properties

        #region Constructors
        /// <summary>
        /// HttpClientConfiguration Constructor
        /// </summary>
        public HttpClientConfiguration(
            IConfigurationSection configurationSection
        )
        {
            _configurationSection = configurationSection;
        }
        #endregion Constructors
    }
}

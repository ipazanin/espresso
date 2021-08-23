// SpaConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class SpaConfiguration
    {
        private readonly IConfigurationSection _configuration;

        /// <summary>
        /// 
        /// </summary>
        public string SpaProxyServerUrl => _configuration.GetValue<string>("SpaProxyServerUrl");

        /// <summary>
        /// 
        /// </summary>
        public bool UseSpaProxyServer => _configuration.GetValue<bool>("UseSpaProxyServer");

        /// <summary>
        /// 
        /// </summary>
        public bool EnableCors => _configuration.GetValue<bool>("EnableCors");

        /// <summary>
        /// Initializes a new instance of the <see cref="SpaConfiguration"/> class.
        /// </summary>
        /// <param name="configuration"></param>
        public SpaConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
    }
}

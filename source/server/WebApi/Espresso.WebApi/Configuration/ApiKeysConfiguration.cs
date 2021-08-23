// ApiKeysConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiKeysConfiguration
    {
        private readonly IConfigurationSection _configuration;

        /// <summary>
        /// 
        /// </summary>
        public string AndroidApiKey => _configuration.GetValue<string>("Android");

        /// <summary>
        /// 
        /// </summary>
        public string IosApiKey => _configuration.GetValue<string>("Ios");

        /// <summary>
        /// 
        /// </summary>
        public string WebApiKey => _configuration.GetValue<string>("Web");

        /// <summary>
        /// 
        /// </summary>
        public string ParserApiKey => _configuration.GetValue<string>("Parser");

        /// <summary>
        /// 
        /// </summary>
        public string DevAndroidApiKey => _configuration.GetValue<string>("DevAndroid");

        /// <summary>
        ///
        /// </summary>
        public string DevIosApiKey => _configuration.GetValue<string>("DevIos");

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiKeysConfiguration"/> class.
        /// </summary>
        /// <param name="configuration"></param>
        public ApiKeysConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
    }
}

// ApiKeysConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.Extensions.Configuration;

namespace Espresso.Dashboard.Configuration
{
    public class ApiKeysConfiguration
    {
        private readonly IConfigurationSection _configuration;

        public string ParserApiKey => _configuration.GetValue<string>("Parser");

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

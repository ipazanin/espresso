// DatabaseConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi.Configuration
{
    /// <summary>
    ///
    /// </summary>
    public class DatabaseConfiguration
    {
        private readonly IConfigurationSection _configuration;

        /// <summary>
        ///
        /// </summary>
        public string EspressoDatabaseConnectionString => _configuration.GetValue<string>("EspressoDatabaseConnectionString");

        /// <summary>
        ///
        /// </summary>
        public int CommandTimeoutInSeconds => _configuration.GetValue<int>("CommandTimeoutInSeconds");

        /// <summary>
        ///
        /// </summary>
        public QueryTrackingBehavior QueryTrackingBehavior => _configuration.GetValue<QueryTrackingBehavior>("QueryTrackingBehavior");

        /// <summary>
        ///
        /// </summary>
        public bool EnableDetailedErrors => _configuration.GetValue<bool>("EnableDetailedErrors");

        /// <summary>
        ///
        /// </summary>
        public bool EnableSensitiveDataLogging => _configuration.GetValue<bool>("EnableSensitiveDataLogging");

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConfiguration"/> class.
        /// </summary>
        /// <param name="configuration"></param>
#pragma warning disable SA1201 // Elements should appear in the correct order
        public DatabaseConfiguration(IConfigurationSection configuration)
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
            _configuration = configuration;
        }
    }
}

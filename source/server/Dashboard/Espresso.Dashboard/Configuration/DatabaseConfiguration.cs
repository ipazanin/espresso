// DatabaseConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Espresso.Dashboard.Configuration
{
    public class DatabaseConfiguration
    {
        private readonly IConfigurationSection _configuration;

        public string EspressoDatabaseConnectionString => _configuration.GetValue<string>("EspressoDatabaseConnectionString");

        public string EspressoIdentityDatabaseConnectionString => _configuration.GetValue<string>("EspressoIdentityDatabaseConnectionString");

        public int CommandTimeoutInSeconds => _configuration.GetValue<int>("CommandTimeoutInSeconds");

        public QueryTrackingBehavior QueryTrackingBehavior => _configuration.GetValue<QueryTrackingBehavior>("QueryTrackingBehavior");

        public bool EnableDetailedErrors => _configuration.GetValue<bool>("EnableDetailedErrors");

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

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Espresso.Dashboard.Configuration
{
    public class DatabaseConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configuration;
        #endregion

        #region Properties
        public string EspressoDatabaseConnectionString => _configuration.GetValue<string>("EspressoDatabaseConnectionString");

        public string EspressoIdentityDatabaseConnectionString => _configuration.GetValue<string>("EspressoIdentityDatabaseConnectionString");

        public int CommandTimeoutInSeconds => _configuration.GetValue<int>("CommandTimeoutInSeconds");

        public QueryTrackingBehavior QueryTrackingBehavior => _configuration.GetValue<QueryTrackingBehavior>("QueryTrackingBehavior");

        public bool EnableDetailedErrors => _configuration.GetValue<bool>("EnableDetailedErrors");

        public bool EnableSensitiveDataLogging => _configuration.GetValue<bool>("EnableSensitiveDataLogging");
        #endregion

        #region Constructors
        public DatabaseConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}

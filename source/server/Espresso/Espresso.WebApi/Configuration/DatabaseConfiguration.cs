using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class DatabaseConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configuration;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string EspressoDatabaseConnectionString => _configuration.GetValue<string>("EspressoDatabaseConnectionString");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int CommandTimeoutInSeconds => _configuration.GetValue<int>("CommandTimeoutInSeconds");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public QueryTrackingBehavior QueryTrackingBehavior => _configuration.GetValue<QueryTrackingBehavior>("QueryTrackingBehavior");


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool EnableDetailedErrors => _configuration.GetValue<bool>("EnableDetailedErrors");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool EnableSensitiveDataLogging => _configuration.GetValue<bool>("EnableSensitiveDataLogging");
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public DatabaseConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}

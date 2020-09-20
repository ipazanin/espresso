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
        public string ConnectionString => _configuration.GetValue<string>("DefaultConnectionString");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int CommandTimeoutInSeconds => _configuration.GetValue<int>("CommandTimeoutInSeconds");
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

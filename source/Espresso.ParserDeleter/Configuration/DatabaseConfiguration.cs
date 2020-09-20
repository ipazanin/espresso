using Microsoft.Extensions.Configuration;

namespace Espresso.ParserDeleter.Configuration
{
    public class DatabaseConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configuration;
        #endregion

        #region Properties
        public string ConnectionString => _configuration.GetValue<string>("DefaultConnectionString");

        public int CommandTimeoutInSeconds => _configuration.GetValue<int>("CommandTimeoutInSeconds");
        #endregion

        #region Constructors
        public DatabaseConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}

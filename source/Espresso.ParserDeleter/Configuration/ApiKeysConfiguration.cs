using Microsoft.Extensions.Configuration;

namespace Espresso.ParserDeleter.Configuration
{
    public class ApiKeysConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configuration;
        #endregion

        #region Properties
        public string ParserApiKey => _configuration.GetValue<string>("Parser");
        #endregion

        #region Constructors
        public ApiKeysConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}

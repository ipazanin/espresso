using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiKeysConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configuration;
        #endregion

        #region Properties

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
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public ApiKeysConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}

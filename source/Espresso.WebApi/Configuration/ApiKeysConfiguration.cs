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
        /// <returns></returns>
        public string AndroidApiKey => _configuration.GetValue<string>("Android");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string IosApiKey => _configuration.GetValue<string>("Ios");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string WebApiKey => _configuration.GetValue<string>("Web");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ParserApiKey => _configuration.GetValue<string>("Parser");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string DevAndroidApiKey => _configuration.GetValue<string>("DevAndroid");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class SpaConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configuration;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string SpaProxyServerUrl => _configuration.GetValue<string>("SpaProxyServerUrl");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool UseSpaProxyServer => _configuration.GetValue<bool>("UseSpaProxyServer");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool EnableCors => _configuration.GetValue<bool>("EnableCors");
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public SpaConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}

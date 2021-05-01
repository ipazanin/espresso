using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class TrendingScoreConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configuration;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public int HalfOfMaxTrendingScoreValue => _configuration.GetValue<int>("HalfOfMaxTrendingScoreValue");

        /// <summary>
        /// 
        /// </summary>
        public decimal AgeWeight => _configuration.GetValue<decimal>("AgeWeight");
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public TrendingScoreConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}

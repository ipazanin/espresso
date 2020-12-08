using System;
using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class DateTimeConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configuration;
        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public TimeSpan MaxAgeOfTrendingArticle => TimeSpan.FromHours(
            value: _configuration.GetValue<int>("MaxAgeOfTrendingArticlesInHours")
        );

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public TimeSpan MaxAgeOfFeaturedArticle => TimeSpan.FromHours(
            value: _configuration.GetValue<int>("MaxAgeOfFeaturedArticlesInHours")
        );

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public TimeSpan MaxAgeOfArticle => TimeSpan.FromDays(
            value: _configuration.GetValue<int>("MaxAgeOfArticlesInDays")
        );

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public TimeSpan MaxAgeOfNewNewsPortal => TimeSpan.FromDays(
            value: _configuration.GetValue<int>("MaxAgeOfNewNewsPortalInDays")
        );
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public DateTimeConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}

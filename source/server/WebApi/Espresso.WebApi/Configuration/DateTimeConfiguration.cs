// DateTimeConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.Extensions.Configuration;
using System;

namespace Espresso.WebApi.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class DateTimeConfiguration
    {
        private readonly IConfigurationSection _configuration;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeConfiguration"/> class.
        /// </summary>
        /// <param name="configuration"></param>
        public DateTimeConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
    }
}

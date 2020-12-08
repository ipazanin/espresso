using System;
using System.Collections.Generic;
using System.Diagnostics;
using Espresso.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class AppConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configuration;
        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public AppEnvironment AppEnvironment => _configuration.GetValue<AppEnvironment>("Environment");


        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public int NewNewsPortalsPosition => _configuration.GetValue<int>("NewNewsPortalsPosition");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Version => _configuration.GetValue<string>("Version");

        /// <summary>
        /// 
        /// </summary>
        public ApiVersion ApiVersion => new ApiVersion(
            majorVersion: _configuration.GetValue<int>("MajorVersion"),
            minorVersion: _configuration.GetValue<int>("MinorVersion")
        );

        /// <summary>
        /// All Api Versions
        /// </summary>
        /// <value></value>
        public IEnumerable<ApiVersion> ApiVersions => new[]
        {
            ApiVersion,
            new ApiVersion(2, 0),
            new ApiVersion(1, 4),
            new ApiVersion(1, 3),
            new ApiVersion(1, 2),
        };

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TimeSpan Uptime => DateTime.UtcNow - Process.GetCurrentProcess().StartTime.ToUniversalTime();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int FeaturedArticlesTake => _configuration.GetValue<int>("FeaturedArticlesTake");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string SlackWebHook => _configuration.GetValue<string>("SlackWebHook");

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public int MaxHttpHandlerRetries => _configuration.GetValue<int>("MaxHttpHandlerRetries");
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public AppConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}

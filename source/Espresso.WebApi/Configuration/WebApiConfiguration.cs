using System;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class WebApiConfiguration : IWebApiConfiguration
    {

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public AppVersionConfiguration AppVersionConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public AppConfiguration AppConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public DatabaseConfiguration DatabaseConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public ApiKeysConfiguration ApiKeysConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public SpaConfiguration SpaConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        public AppEnvironment AppEnvironment => AppConfiguration.AppEnvironment;

        /// <summary>
        /// 
        /// </summary>
        public string Version => AppVersionConfiguration.Version;

        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString => DatabaseConfiguration.ConnectionString;

        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public WebApiConfiguration(IConfiguration configuration)
        {
            AppConfiguration = new AppConfiguration(configuration.GetSection("AppConfiguration"));
            AppVersionConfiguration = new AppVersionConfiguration();
            DatabaseConfiguration = new DatabaseConfiguration(configuration.GetSection("DatabaseConfiguration"));
            ApiKeysConfiguration = new ApiKeysConfiguration(configuration.GetSection("ApiKeysConfiguration"));
            SpaConfiguration = new SpaConfiguration(configuration.GetSection("SpaConfiguration"));
        }
        #endregion
    }
}

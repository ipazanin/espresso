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
        #region Fields
        private readonly IConfiguration _configuration;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString => _configuration.GetConnectionString("DefaultConnectionString");

        /// <summary>
        /// 
        /// </summary>
        public string Version =>
            $"{ApiVersionConstants.CurrentMajorVersion}." +
            $"{ApiVersionConstants.CurrentMinorVersion}." +
            $"{ApiVersionConstants.CurrentFixVersion}";

        /// <summary>
        /// 
        /// </summary>
        public ApiVersion EspressoWebApiVersion_1_2 => new ApiVersion(1, 2);

        /// <summary>
        /// 
        /// </summary>
        public ApiVersion EspressoWebApiVersion_1_3 => new ApiVersion(1, 3);

        /// <summary>
        /// 
        /// </summary>
        public ApiVersion EspressoWebApiCurrentVersion =>
            new ApiVersion(
                majorVersion: ApiVersionConstants.CurrentMajorVersion,
                minorVersion: ApiVersionConstants.CurrentMinorVersion
            );

        /// <summary>
        /// 
        /// </summary>
        public AppEnvironment AppEnvironment => _configuration.GetValue<AppEnvironment>("AppConfiguration:Environment");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string SpaProxyServerUrl => _configuration.GetValue<string>("AppConfiguration:SpaProxyServerUrl");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool UseSpaProxyServer => _configuration.GetValue<bool>("AppConfiguration:UseSpaProxyServer");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool EnableCors => _configuration.GetValue<bool>("AppConfiguration:EnableCors");

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public int NewNewsPortalsPosition => _configuration.GetValue<int>("AppConfiguration:NewNewsPortalsPosition");

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public TimeSpan MaxAgeOfTrendingArticle => TimeSpan.FromHours(
            value: _configuration.GetValue<int>("AppConfiguration:MaxAgeOfTrendingArticlesInHours")
        );

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public TimeSpan MaxAgeOfFeaturedArticle => TimeSpan.FromHours(
            value: _configuration.GetValue<int>("AppConfiguration:MaxAgeOfFeaturedArticlesInHours")
        );

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string AndroidApiKey => _configuration.GetValue<string>("AppConfiguration:ApiKeys:Android");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string IosApiKey => _configuration.GetValue<string>("AppConfiguration:ApiKeys:Ios");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string WebApiKey => _configuration.GetValue<string>("AppConfiguration:ApiKeys:Web");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ParserApiKey => _configuration.GetValue<string>("AppConfiguration:ApiKeys:Parser");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string DevAndroidApiKey => _configuration.GetValue<string>("AppConfiguration:ApiKeys:DevAndroid");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string DevIosApiKey => _configuration.GetValue<string>("AppConfiguration:ApiKeys:DevIos");
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public WebApiConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}

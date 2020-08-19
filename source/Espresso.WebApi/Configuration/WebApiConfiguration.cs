using System;
using System.Collections.Generic;
using System.Globalization;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Domain.Utilities;
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
        public AppEnvironment AppEnvironment => EnumUtility.GetEnumOrDefault(
            enumValue: _configuration["AppConfiguration:Environment"],
            defaultValue: AppEnvironment.Prod
        );

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string SpaProxyServerUrl => _configuration["AppConfiguration:SpaProxyServerUrl"];

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public int NewNewsPortalsPosition => int.Parse(
            s: _configuration["AppConfiguration:NewNewsPortalsPosition"],
            style: NumberStyles.Integer,
            provider: CultureInfo.InvariantCulture
        );

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public TimeSpan MaxAgeOfTrendingArticle => TimeSpan.FromHours(
            value: int.Parse(
                s: _configuration["AppConfiguration:MaxAgeOfTrendingArticlesInHours"],
                style: NumberStyles.Integer,
                provider: CultureInfo.InvariantCulture
            )
        );

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string AndroidApiKey => _configuration["AppConfiguration:ApiKeys:Android"];

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string IosApiKey => _configuration["AppConfiguration:ApiKeys:Ios"];

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string WebApiKey => _configuration["AppConfiguration:ApiKeys:Web"];

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ParserApiKey => _configuration["AppConfiguration:ApiKeys:Parser"];
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

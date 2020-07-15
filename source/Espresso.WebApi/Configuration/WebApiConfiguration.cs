using System;
using System.Collections.Generic;
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
        public string ConnectionString => _configuration.GetConnectionString(ConfigurationKeyNameConstants.DefaultConnectionStringKeyName);

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
        public IEnumerable<string> ApiKeys => _configuration["AppConfiguration:ApiKeys"].Split(",", StringSplitOptions.RemoveEmptyEntries);
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

using System;
using Espresso.Common.Enums;
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

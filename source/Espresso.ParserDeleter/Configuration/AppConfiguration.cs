﻿using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Microsoft.Extensions.Configuration;

namespace Espresso.ParserDeleter.Configuration
{
    public class AppConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configuration;
        #endregion

        #region Properties
        public string RssFeedParserVersion =>
            $"{ApiVersionConstants.CurrentMajorVersion}." +
            $"{ApiVersionConstants.CurrentMinorVersion}." +
            $"{ApiVersionConstants.CurrentFixVersion}";

        public string RssFeedParserMajorMinorVersion =>
            $"{ApiVersionConstants.CurrentMajorVersion}." +
            $"{ApiVersionConstants.CurrentMinorVersion}";

        public string ServerUrl => _configuration.GetValue<string>("ServerUrl");

        public AppEnvironment AppEnvironment => _configuration.GetValue<AppEnvironment>("Environment");

        public string Version => RssFeedParserVersion;
        #endregion

        #region Constructors
        public AppConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}

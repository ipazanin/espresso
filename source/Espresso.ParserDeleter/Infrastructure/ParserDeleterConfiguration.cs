using System;
using System.Collections.Generic;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Domain.Utilities;
using Microsoft.Extensions.Configuration;

namespace Espresso.Workers.ParserDeleter.Infrastructure
{
    public class ParserDeleterConfiguration : IParserDeleterConfiguration
    {
        #region Fields
        private readonly IConfiguration _configuration;
        #endregion

        #region Properties
        public string ConnectionString => _configuration.GetConnectionString(ConfigurationKeyNameConstants.DefaultConnectionStringKeyName);

        public string RssFeedParserVersion =>
            $"{ApiVersionConstants.CurrentMajorVersion}." +
            $"{ApiVersionConstants.CurrentMinorVersion}." +
            $"{ApiVersionConstants.CurrentFixVersion}";

        public string RssFeedParserMajorMinorVersion =>
            $"{ApiVersionConstants.CurrentMajorVersion}." +
            $"{ApiVersionConstants.CurrentMinorVersion}";

        public string ServerUrl => _configuration["AppConfiguration:ServerUrl"] ?? "";

        public AppEnvironment AppEnvironment => EnumUtility.GetEnumOrDefault(
            enumValue: _configuration["AppConfiguration:Environment"],
            defaultValue: AppEnvironment.Undefined
        );

        public string Version => RssFeedParserVersion;

        public IEnumerable<string> ApiKeys => _configuration["AppConfiguration:ApiKeys"].Split(",", StringSplitOptions.RemoveEmptyEntries);

        #region Durations
        public TimeSpan MaxAgeOfOldArticles => DateTimeConstants.MaxAgeOfArticle;

        public TimeSpan CancellationTokenExpirationDuration => TimeSpan.FromMinutes(5);

        public TimeSpan WaitDurationBetweenCommands => TimeSpan.FromSeconds(30);

        public TimeSpan WaitDurationAfterErrors => TimeSpan.FromSeconds(30);

        public TimeSpan WaitDurationAfterWebServerRequestError => TimeSpan.FromSeconds(5);
        #endregion

        #endregion

        #region Constructors
        public ParserDeleterConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}

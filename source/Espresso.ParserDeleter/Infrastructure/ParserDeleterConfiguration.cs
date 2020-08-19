using System;
using System.Collections.Generic;
using System.Globalization;
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
        public string ConnectionString => _configuration.GetConnectionString("DefaultConnectionString");

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

        public string ParserApiKey => _configuration["AppConfiguration:ApiKeys:Parser"];

        #region Durations
        public TimeSpan MaxAgeOfOldArticles => DateTimeConstants.MaxAgeOfArticle;

        public TimeSpan CancellationTokenExpirationDuration =>
            TimeSpan.FromSeconds(
                value: int.Parse(
                    s: _configuration["AppConfiguration:WaitDurationsInSeconds:CancellationTokenExpiration"],
                    style: NumberStyles.Integer,
                    provider: CultureInfo.InvariantCulture
                )
            );

        public TimeSpan WaitDurationBetweenCommands =>
            TimeSpan.FromSeconds(
                value: int.Parse(
                    s: _configuration["AppConfiguration:WaitDurationsInSeconds:BetweenCommands"],
                    style: NumberStyles.Integer,
                    provider: CultureInfo.InvariantCulture
                )
            );

        public TimeSpan WaitDurationAfterErrors =>
            TimeSpan.FromSeconds(
                value: int.Parse(
                    s: _configuration["AppConfiguration:WaitDurationsInSeconds:AfterErrors"],
                    style: NumberStyles.Integer,
                    provider: CultureInfo.InvariantCulture
                )
            );

        public TimeSpan WaitDurationAfterWebServerRequestError =>
            TimeSpan.FromSeconds(
                value: int.Parse(
                    s: _configuration["AppConfiguration:WaitDurationsInSeconds:AfterWebServerRequestError"],
                    style: NumberStyles.Integer,
                    provider: CultureInfo.InvariantCulture
                )
            );

        public TimeSpan WaitDurationBeforeStartup =>
            TimeSpan.FromSeconds(
                value: int.Parse(
                    s: _configuration["AppConfiguration:WaitDurationsInSeconds:BeforeStartup"],
                    style: NumberStyles.Integer,
                    provider: CultureInfo.InvariantCulture
                )
            );

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

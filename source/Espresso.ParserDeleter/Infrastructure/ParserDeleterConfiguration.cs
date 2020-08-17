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

        public IEnumerable<string> ApiKeys => _configuration["AppConfiguration:ApiKeys"].Split(",", StringSplitOptions.RemoveEmptyEntries);

        #region Durations
        public TimeSpan MaxAgeOfOldArticles => DateTimeConstants.MaxAgeOfArticle;

        public TimeSpan CancellationTokenExpirationDuration =>
            // string.IsNullOrEmpty(_configuration["AppConfiguration:WaitDurationsInSeconds:CancellationTokenExpiration"]) ?
            // TimeSpan.FromMinutes(5) :
            TimeSpan.FromSeconds(
                value: int.Parse(
                    s: _configuration["AppConfiguration:WaitDurationsInSeconds:CancellationTokenExpiration"],
                    style: NumberStyles.Integer,
                    provider: CultureInfo.InvariantCulture
                )
            );

        public TimeSpan WaitDurationBetweenCommands =>
            // string.IsNullOrEmpty(_configuration["AppConfiguration:WaitDurationsInSeconds:BetweenCommands"]) ?
            // TimeSpan.FromSeconds(30) :
            TimeSpan.FromSeconds(
                value: int.Parse(
                    s: _configuration["AppConfiguration:WaitDurationsInSeconds:BetweenCommands"],
                    style: NumberStyles.Integer,
                    provider: CultureInfo.InvariantCulture
                )
            );

        public TimeSpan WaitDurationAfterErrors =>
            // string.IsNullOrEmpty(_configuration["AppConfiguration:WaitDurationsInSeconds:AfterErrors"]) ?
            // TimeSpan.FromSeconds(30) :
            TimeSpan.FromSeconds(
                value: int.Parse(
                    s: _configuration["AppConfiguration:WaitDurationsInSeconds:AfterErrors"],
                    style: NumberStyles.Integer,
                    provider: CultureInfo.InvariantCulture
                )
            );

        public TimeSpan WaitDurationAfterWebServerRequestError =>
            // string.IsNullOrEmpty(_configuration["AppConfiguration:WaitDurationsInSeconds:AfterWebServerRequestError"]) ?
            // TimeSpan.FromSeconds(5) :
            TimeSpan.FromSeconds(
                value: int.Parse(
                    s: _configuration["AppConfiguration:WaitDurationsInSeconds:AfterWebServerRequestError"],
                    style: NumberStyles.Integer,
                    provider: CultureInfo.InvariantCulture
                )
            );

        public TimeSpan WaitDurationBeforeStartup =>
            // string.IsNullOrEmpty(_configuration["AppConfiguration:WaitDurationsInSeconds:BeforeStartup"]) ?
            // TimeSpan.FromSeconds(30) :
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

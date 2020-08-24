using System;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
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

        public string ServerUrl => _configuration.GetValue<string>("AppConfiguration:ServerUrl");

        public AppEnvironment AppEnvironment => _configuration.GetValue<AppEnvironment>("AppConfiguration:Environment");

        public string Version => RssFeedParserVersion;

        public string ParserApiKey => _configuration.GetValue<string>("AppConfiguration:ApiKeys:Parser");

        #region Durations
        public TimeSpan MaxAgeOfOldArticles => DateTimeConstants.MaxAgeOfArticle;

        public TimeSpan CancellationTokenExpirationDuration =>
            TimeSpan.FromSeconds(
                value: _configuration.GetValue<int>("AppConfiguration:WaitDurationsInSeconds:CancellationTokenExpiration")
            );

        public TimeSpan WaitDurationBetweenCommands =>
            TimeSpan.FromSeconds(
                value: _configuration.GetValue<int>("AppConfiguration:WaitDurationsInSeconds:BetweenCommands")
            );

        public TimeSpan WaitDurationAfterErrors =>
            TimeSpan.FromSeconds(
                value: _configuration.GetValue<int>("AppConfiguration:WaitDurationsInSeconds:AfterErrors")
            );

        public TimeSpan WaitDurationAfterWebServerRequestError =>
            TimeSpan.FromSeconds(
                value: _configuration.GetValue<int>("AppConfiguration:WaitDurationsInSeconds:AfterWebServerRequestError")
            );

        public TimeSpan WaitDurationBeforeStartup =>
            TimeSpan.FromSeconds(
                value: _configuration.GetValue<int>("AppConfiguration:WaitDurationsInSeconds:BeforeStartup")
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

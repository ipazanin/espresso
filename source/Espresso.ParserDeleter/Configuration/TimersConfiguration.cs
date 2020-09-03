using System;
using Espresso.Common.Constants;
using Microsoft.Extensions.Configuration;

namespace Espresso.ParserDeleter.Configuration
{
    public class TimersConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configuration;
        #endregion

        #region Properties
        public TimeSpan MaxAgeOfOldArticles => DateTimeConstants.MaxAgeOfArticle;

        public TimeSpan CancellationTokenExpirationDuration =>
            TimeSpan.FromSeconds(
                value: _configuration.GetValue<int>("CancellationTokenExpiration")
            );

        public TimeSpan WaitDurationBetweenCommands =>
            TimeSpan.FromSeconds(
                value: _configuration.GetValue<int>("BetweenCommands")
            );

        public TimeSpan WaitDurationAfterErrors =>
            TimeSpan.FromSeconds(
                value: _configuration.GetValue<int>("AfterErrors")
            );

        public TimeSpan WaitDurationAfterWebServerRequestError =>
            TimeSpan.FromSeconds(
                value: _configuration.GetValue<int>("AfterWebServerRequestError")
            );

        public TimeSpan WaitDurationBeforeStartup =>
            TimeSpan.FromSeconds(
                value: _configuration.GetValue<int>("BeforeStartup")
            );

        #endregion

        #region Constructors
        public TimersConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}

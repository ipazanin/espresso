using System;
using Microsoft.Extensions.Configuration;

namespace Espresso.ParserDeleter.Configuration
{
    public class DateTimeConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configuration;
        #endregion

        #region Properties
        public TimeSpan MaxAgeOfArticles =>
            TimeSpan.FromDays(
                value: _configuration.GetValue<int>("MaxAgeOfArticlesInDays")
            );

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
        public DateTimeConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}

using System;
using Espresso.Common.Configuration;

namespace Espresso.Workers.ParserDeleter.Infrastructure
{
    public interface IParserDeleterConfiguration : ICommonConfiguration
    {
        public string RssFeedParserVersion { get; }

        public string RssFeedParserMajorMinorVersion { get; }

        public string ServerUrl { get; }

        #region Durations

        public TimeSpan MaxAgeOfOldArticles { get; }

        public TimeSpan CancellationTokenExpirationDuration { get; }

        public TimeSpan WaitDurationBetweenCommands { get; }

        public TimeSpan WaitDurationAfterErrors { get; }

        public TimeSpan WaitDurationAfterWebServerRequestError { get; }

        #endregion
    }
}

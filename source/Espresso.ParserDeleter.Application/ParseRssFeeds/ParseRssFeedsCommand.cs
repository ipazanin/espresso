using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using MediatR;

namespace Espresso.ParserDeleter.ParseRssFeeds
{
    public class ParseRssFeedsCommand : Request<Unit>
    {
        public TimeSpan MaxAgeOfArticle { get; }

        public string ParserApiKey { get; }

        public TimeSpan WaitDurationAfterWebServerRequestError { get; }

        public string ServerUrl { get; }

        public ParseRssFeedsCommand(
            TimeSpan maxAgeOfArticle,
            string parserApiKey,
            TimeSpan waitDurationAfterWebServerRequestError,
            string serverUrl,
            string currentEspressoWebApiVersion,
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            AppEnvironment appEnvironment
        ) : base(
            currentEspressoWebApiVersion: currentEspressoWebApiVersion,
            targetedEspressoWebApiVersion: targetedEspressoWebApiVersion,
            consumerVersion: consumerVersion,
            deviceType: deviceType,
            appEnvironment: appEnvironment,
            Event.ParseRssFeedsCommand
        )
        {
            MaxAgeOfArticle = maxAgeOfArticle;
            ParserApiKey = parserApiKey;
            WaitDurationAfterWebServerRequestError = waitDurationAfterWebServerRequestError;
            ServerUrl = serverUrl;
        }

        public override string ToString()
        {
            return $"{nameof(MaxAgeOfArticle)}:{MaxAgeOfArticle}, " +
                $"{nameof(WaitDurationAfterWebServerRequestError)}:{WaitDurationAfterWebServerRequestError}, " +
                $"{nameof(ServerUrl)}:{ServerUrl}";
        }
    }
}

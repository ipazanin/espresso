using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.ParserDeleter.ParseRssFeeds
{
    public class ParseRssFeedsCommand : Request<ParseRssFeedsCommandResponse>
    {
        public TimeSpan MaxAgeOfArticle { get; }

        public string ParserApiKey { get; }


        public string ServerUrl { get; }

        public ParseRssFeedsCommand(
            TimeSpan maxAgeOfArticle,
            string parserApiKey,
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
            ServerUrl = serverUrl;
        }

        public override string ToString()
        {
            return $"{nameof(MaxAgeOfArticle)}:{MaxAgeOfArticle}, " +
                $"{nameof(ServerUrl)}:{ServerUrl}";
        }
    }
}

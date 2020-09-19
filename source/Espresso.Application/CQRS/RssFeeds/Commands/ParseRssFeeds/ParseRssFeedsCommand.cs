using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.Application.CQRS.RssFeeds.Commands.ParseRssFeeds
{
    public class ParseRssFeedsCommand : Request<ParseRssFeedsCommandResponse>
    {
        public TimeSpan MaxAgeOfArticle { get; }

        public ParseRssFeedsCommand(
            TimeSpan maxAgeOfArticle,
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
        }

        public override string ToString()
        {
            return $"{nameof(MaxAgeOfArticle)}:{MaxAgeOfArticle}";
        }
    }
}

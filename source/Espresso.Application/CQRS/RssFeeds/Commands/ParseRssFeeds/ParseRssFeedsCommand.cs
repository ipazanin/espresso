﻿using Espresso.Application.Infrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.Application.CQRS.RssFeeds.Commands.ParseRssFeeds
{
    public class ParseRssFeedsCommand : Request<ParseRssFeedsCommandResponse>
    {
        public ParseRssFeedsCommand(
            string currentEspressoWebApiVersion,
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType
        ) : base(
            currentEspressoWebApiVersion: currentEspressoWebApiVersion,
            targetedEspressoWebApiVersion: targetedEspressoWebApiVersion,
          consumerVersion: consumerVersion,
            deviceType: deviceType,
            Event.ParseRssFeedsCommand
        )
        {
        }

        public override string ToString()
        {
            return "";
        }
    }
}
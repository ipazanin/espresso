using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.ParserDeleter.ParseRssFeeds
{
    public record ParseRssFeedsCommand : Request<ParseRssFeedsCommandResponse>
    {
        public TimeSpan MaxAgeOfArticle { get; init; }

        public string ParserApiKey { get; init; } = "";

        public string ServerUrl { get; init; } = "";
    }
}

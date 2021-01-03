using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.ParserDeleter.ParseRssFeeds
{
    public record ParseRssFeedsCommand : Request<ParseRssFeedsCommandResponse>
    {
    }
}

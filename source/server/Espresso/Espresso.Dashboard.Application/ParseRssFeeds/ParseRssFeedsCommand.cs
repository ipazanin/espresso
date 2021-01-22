using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.Dashboard.ParseRssFeeds
{
    public record ParseRssFeedsCommand : Request<ParseRssFeedsCommandResponse>
    {
    }
}

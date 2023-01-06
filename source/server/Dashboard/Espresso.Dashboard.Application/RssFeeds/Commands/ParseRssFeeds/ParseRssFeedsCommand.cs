// ParseRssFeedsCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Domain.Entities;

namespace Espresso.Dashboard.Application.RssFeeds.Commands.ParseRssFeeds;

public record ParseRssFeedsCommand : Request<ParseRssFeedsCommandResponse>
{
    public IEnumerable<RssFeed> RssFeeds { get; init; } = Array.Empty<RssFeed>();

    public IEnumerable<Category> Categories { get; init; } = Array.Empty<Category>();

    public IDictionary<Guid, Article> Articles { get; init; } = new Dictionary<Guid, Article>();
}

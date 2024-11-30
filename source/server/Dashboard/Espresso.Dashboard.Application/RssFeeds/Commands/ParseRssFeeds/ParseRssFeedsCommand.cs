// ParseRssFeedsCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Domain.Entities;

namespace Espresso.Dashboard.Application.RssFeeds.Commands.ParseRssFeeds;

public record ParseRssFeedsCommand : Request<ParseRssFeedsCommandResponse>
{
    public IReadOnlyList<RssFeed> RssFeeds { get; init; } = [];

    public IReadOnlyList<Category> Categories { get; init; } = [];

    public IDictionary<Guid, Article> Articles { get; init; } = new Dictionary<Guid, Article>();
}

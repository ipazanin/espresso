// ParseRssFeedsCommand.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Espresso.Dashboard.ParseRssFeeds
{
    public record ParseRssFeedsCommand : Request<ParseRssFeedsCommandResponse>
    {
        public IEnumerable<RssFeed> RssFeeds { get; init; } = Array.Empty<RssFeed>();

        public IEnumerable<Category> Categories { get; init; } = Array.Empty<Category>();

        public IDictionary<Guid, Article> Articles { get; init; } = new Dictionary<Guid, Article>();

        public ISet<Guid> SubordinateArticleIds { get; init; } = new HashSet<Guid>();
    }
}

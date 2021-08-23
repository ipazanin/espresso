// ParseRssFeedsCommandResponse.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Dashboard.ParseRssFeeds
{
    public record ParseRssFeedsCommandResponse
    {
        public int CreatedArticles { get; init; }

        public int UpdatedArticles { get; init; }
    }
}

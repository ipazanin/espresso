// ParseRssFeedsCommandResponse.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.Dashboard.ParseRssFeeds;

public record ParseRssFeedsCommandResponse
{
    public int CreatedArticles { get; init; }

    public int UpdatedArticles { get; init; }
}

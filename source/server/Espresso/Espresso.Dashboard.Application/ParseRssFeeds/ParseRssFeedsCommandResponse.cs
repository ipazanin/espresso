namespace Espresso.Dashboard.ParseRssFeeds
{
    public record ParseRssFeedsCommandResponse
    {
        public int CreatedArticles { get; init; }

        public int UpdatedArticles { get; init; }
    }
}

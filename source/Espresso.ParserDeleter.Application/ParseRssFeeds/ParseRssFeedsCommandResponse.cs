namespace Espresso.ParserDeleter.ParseRssFeeds
{
    public record ParseRssFeedsCommandResponse
    {
        public int CreatedArticles { get; init; }

        public int UpdatedArticles { get; init; }
    }
}

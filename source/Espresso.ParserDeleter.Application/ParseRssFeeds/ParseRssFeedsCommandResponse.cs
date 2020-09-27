namespace Espresso.ParserDeleter.ParseRssFeeds
{
    public class ParseRssFeedsCommandResponse
    {
        public int CreatedArticles { get; }

        public int UpdatedArticles { get; }

        public ParseRssFeedsCommandResponse(int createdArticles, int updatedArticles)
        {
            CreatedArticles = createdArticles;
            UpdatedArticles = updatedArticles;
        }

        public override string ToString()
        {
            return $"{nameof(CreatedArticles)}:{CreatedArticles}, " +
                $"{nameof(UpdatedArticles)}:{UpdatedArticles}";
        }
    }
}

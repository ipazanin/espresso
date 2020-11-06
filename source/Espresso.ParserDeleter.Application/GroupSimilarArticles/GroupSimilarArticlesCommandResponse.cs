namespace Espresso.ParserDeleter.Application.GroupSimilarArticles
{
    public record GroupSimilarArticlesCommandResponse
    {
        public int SimilarArticlesCount { get; init; }
    }
}
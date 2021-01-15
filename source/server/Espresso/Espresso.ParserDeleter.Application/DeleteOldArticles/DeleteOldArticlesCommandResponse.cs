namespace Espresso.ParserDeleter.Application.DeleteOldArticles
{
    public record DeleteOldArticlesCommandResponse
    {
        public int NumberOfDeletedDatabaseArticles { get; init; }
        public int NumberOfDeletedMemoryCacheArticles { get; init; }
    }
}

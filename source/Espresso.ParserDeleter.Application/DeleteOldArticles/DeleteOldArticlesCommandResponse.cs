namespace Espresso.ParserDeleter.Application.DeleteOldArticles
{
    public record DeleteOldArticlesCommandResponse
    {
        public int NumberOfDeletedDatabaseAricles { get; init; }
        public int NumberOfDeletedMemoryCacheAricles { get; init; }
    }
}

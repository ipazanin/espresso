namespace Espresso.ParserDeleter.Application.DeleteOldArticles
{
    public record DeleteOldArticlesCommandResponse
    {
        public int NumberOfDeletedAricles { get; init; }
    }
}

namespace Espresso.WebApi.Application.Articles.Commands.UpdateInMemoryArticles
{
    public record UpdateInMemoryArticlesCommandResponse
    {
        public int NumberOfUpdatedArticles { get; init; }
        public int NumberOfCreatedArticles { get; init; }
    }
}

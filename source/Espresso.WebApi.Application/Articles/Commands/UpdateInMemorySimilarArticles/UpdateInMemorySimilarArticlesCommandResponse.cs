namespace Espresso.WebApi.Application
{
    public record UpdateInMemorySimilarArticlesCommandResponse
    {
        public int SimilarArticlesCount { get; init; }
    }
}
namespace Espresso.WebApi.Application.Articles.AutoCompleteArticle
{
    public record AutoCompleteArticleResult
    {
        public string? Title { get; init; }

        public string? Url { get; init; }
    }
}
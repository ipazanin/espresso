// GetTrendingArticlesQueryResponse.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles
{
    public record GetTrendingArticlesQueryResponse
    {
        public IEnumerable<GetTrendingArticlesArticle> Articles { get; init; } = new List<GetTrendingArticlesArticle>();
    }
}

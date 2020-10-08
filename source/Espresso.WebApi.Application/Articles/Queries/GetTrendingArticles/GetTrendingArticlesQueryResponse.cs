using System.Collections.Generic;

namespace Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles
{
    public record GetTrendingArticlesQueryResponse
    {
        public IEnumerable<GetTrendingArticlesArticle> Articles { get; init; } = new List<GetTrendingArticlesArticle>();
    }
}

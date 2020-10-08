using System.Collections.Generic;

namespace Espresso.WebApi.Application.Articles.Queries.GetFeaturedArticles
{
    public record GetFeaturedArticlesQueryResponse
    {
        public IEnumerable<GetFeaturedArticlesArticle> Articles { get; init; } = new List<GetFeaturedArticlesArticle>();
    }
}

using System.Collections.Generic;
using System.Linq;

namespace Espresso.WebApi.Application.Articles.Queries.GetFeaturedArticles
{
    public record GetFeaturedArticlesQueryResponse
    {
        public IEnumerable<GetFeaturedArticlesArticle> Articles { get; init; } = new List<GetFeaturedArticlesArticle>();
    }
}

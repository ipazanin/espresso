using System.Collections.Generic;
using System.Linq;

namespace Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles
{
    public record GetTrendingArticlesQueryResponse
    {
        public IEnumerable<GetTrendingArticlesArticle> Articles { get; init; } = new List<GetTrendingArticlesArticle>();
    }
}

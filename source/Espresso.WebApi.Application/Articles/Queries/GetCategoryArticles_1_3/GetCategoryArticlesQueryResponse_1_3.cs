using System.Collections.Generic;
using System.Linq;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_1_3
{
    public record GetCategoryArticlesQueryResponse_1_3
    {
        public IEnumerable<GetCategoryArticlesArticle_1_3> Articles { get; init; } = new List<GetCategoryArticlesArticle_1_3>();
    }
}

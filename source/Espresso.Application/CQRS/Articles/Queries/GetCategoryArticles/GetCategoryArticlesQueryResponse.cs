using System.Collections.Generic;
using System.Linq;
using Espresso.Application.CQRS.Articles.Queries.Common;

namespace Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles
{
    public class GetCategoryArticlesQueryResponse
    {
        public IEnumerable<ArticleViewModel> Articles { get; } = new List<ArticleViewModel>();

        public GetCategoryArticlesQueryResponse(IEnumerable<ArticleViewModel> articles)
        {
            Articles = articles;
        }

        public override string ToString()
        {
            return $"{nameof(Articles)}:{Articles.Count()}";
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Espresso.Application.ViewModels.ArticleViewModels;

namespace Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles_1_3
{
    public class GetCategoryArticlesQueryResponse_1_3
    {
        public IEnumerable<ArticleViewModel> Articles { get; } = new List<ArticleViewModel>();

        public GetCategoryArticlesQueryResponse_1_3(
            IEnumerable<ArticleViewModel> articles
        )
        {
            Articles = articles;
        }

        public override string ToString()
        {
            return $"{nameof(Articles)}:{Articles.Count()}";
        }
    }
}

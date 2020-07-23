using System.Collections.Generic;
using System.Linq;
using Espresso.Application.ViewModels.ArticleViewModels;

namespace Espresso.Application.CQRS.Articles.Queries.GetTrendingArticles
{
    public class GetTrendingArticlesQueryResponse
    {
        public IEnumerable<ArticleTrendingViewModel> Articles { get; } = new List<ArticleTrendingViewModel>();

        public GetTrendingArticlesQueryResponse(IEnumerable<ArticleTrendingViewModel> articles)
        {
            Articles = articles;
        }

        public override string ToString()
        {
            return $"{nameof(Articles)}:{Articles.Count()}";
        }
    }
}

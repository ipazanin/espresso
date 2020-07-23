using System.Collections.Generic;
using System.Linq;
using Espresso.Application.ViewModels.ArticleViewModels;
using Espresso.Application.ViewModels.NewsPortalViewModels;

namespace Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles
{
    public class GetCategoryArticlesQueryResponse
    {
        public IEnumerable<ArticleViewModel> Articles { get; } = new List<ArticleViewModel>();

        public IEnumerable<NewsPortalViewModel> NewNewsPortals { get; set; }

        public int NewNewsPortalsPosition { get; }

        public GetCategoryArticlesQueryResponse(
            IEnumerable<ArticleViewModel> articles,
            IEnumerable<NewsPortalViewModel> newNewsPortals,
            int newNewsPortalsPosition
        )
        {
            Articles = articles;
            NewNewsPortals = newNewsPortals;
            NewNewsPortalsPosition = newNewsPortalsPosition;
        }

        public override string ToString()
        {
            return $"{nameof(Articles)}:{Articles.Count()}" +
            $"{nameof(NewNewsPortals)}:{NewNewsPortals.Count()}, " +
            $"{nameof(NewNewsPortalsPosition)}:{NewNewsPortalsPosition}";
        }
    }
}

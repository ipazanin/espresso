using System.Collections.Generic;
using System.Linq;
using Espresso.Application.CQRS.Articles.Queries.Common;
using Espresso.Application.CQRS.NewsPortals.Queries.GetNewsPortals;

namespace Espresso.Application.CQRS.Articles.Queries.GetLatestArticles
{
    public class GetLatestArticlesQueryResponse
    {
        #region Properties
        public IEnumerable<ArticleViewModel> Articles { get; }

        public IEnumerable<NewsPortalViewModel> NewNewsPortals { get; set; }

        public int NewNewsPortalsPosition { get; }
        #endregion

        #region Constructors
        public GetLatestArticlesQueryResponse(
            IEnumerable<ArticleViewModel> articles,
            IEnumerable<NewsPortalViewModel> newNewsPortals,
            int newNewsPortalsPosition
        )
        {
            Articles = articles;
            NewNewsPortals = newNewsPortals;
            NewNewsPortalsPosition = newNewsPortalsPosition;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{nameof(Articles)}:{Articles.Count()}, " +
            $"{nameof(NewNewsPortals)}:{NewNewsPortals.Count()}, " +
            $"{nameof(NewNewsPortalsPosition)}:{NewNewsPortalsPosition}";
        }
        #endregion
    }
}

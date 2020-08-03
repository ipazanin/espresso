using System.Collections.Generic;
using System.Linq;

namespace Espresso.Application.CQRS.Articles.Queries.GetLatestArticles
{
    public class GetLatestArticlesQueryResponse
    {
        #region Properties
        public IEnumerable<GetLatestArticlesArticle> Articles { get; }

        public IEnumerable<GetLatestArticlesNewsPortal> NewNewsPortals { get; set; }

        public int NewNewsPortalsPosition { get; }
        #endregion

        #region Constructors
        public GetLatestArticlesQueryResponse(
            IEnumerable<GetLatestArticlesArticle> articles,
            IEnumerable<GetLatestArticlesNewsPortal> newNewsPortals,
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

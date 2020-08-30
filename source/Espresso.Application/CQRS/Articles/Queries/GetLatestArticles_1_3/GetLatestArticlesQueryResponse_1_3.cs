using System.Collections.Generic;
using System.Linq;

namespace Espresso.Application.CQRS.Articles.Queries.GetLatestArticles_1_3
{
    public class GetLatestArticlesQueryResponse_1_3
    {
        #region Properties
        public IEnumerable<GetLatestArticlesArticle_1_3> Articles { get; }
        #endregion

        #region Constructors
        public GetLatestArticlesQueryResponse_1_3(
            IEnumerable<GetLatestArticlesArticle_1_3> articles
        )
        {
            Articles = articles;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{nameof(Articles)}:{Articles.Count()}";
        }
        #endregion
    }
}

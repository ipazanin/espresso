using System.Collections.Generic;
using System.Linq;
using Espresso.Application.CQRS.Articles.Queries.Common;

namespace Espresso.Application.CQRS.Articles.Queries.GetLatestArticles
{
    public class GetLatestArticlesQueryResponse
    {
        #region Properties
        public IEnumerable<ArticleViewModel> Articles { get; }
        #endregion

        #region Constructors
        public GetLatestArticlesQueryResponse(IEnumerable<ArticleViewModel> articles)
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

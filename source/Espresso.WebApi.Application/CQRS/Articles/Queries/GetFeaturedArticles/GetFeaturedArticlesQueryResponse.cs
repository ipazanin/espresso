using System.Collections.Generic;
using System.Linq;

namespace Espresso.WebApi.Application.CQRS.Articles.Queries.GetFeaturedArticles
{
    public class GetFeaturedArticlesQueryResponse
    {
        #region Properties
        public IEnumerable<GetFeaturedArticlesArticle> Articles { get; }
        #endregion

        #region Constructors
        public GetFeaturedArticlesQueryResponse(
            IEnumerable<GetFeaturedArticlesArticle> articles
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

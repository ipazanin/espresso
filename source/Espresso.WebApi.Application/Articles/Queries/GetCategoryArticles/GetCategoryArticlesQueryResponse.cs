using System.Collections.Generic;
using System.Linq;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles
{
    public record GetCategoryArticlesQueryResponse
    {
        #region Properties
        public IEnumerable<GetCategoryArticlesArticle> Articles { get; init; } = new List<GetCategoryArticlesArticle>();

        public IEnumerable<GetCategoryArticlesNewsPortal> NewNewsPortals { get; init; } = new List<GetCategoryArticlesNewsPortal>();

        public int NewNewsPortalsPosition { get; init; }
        #endregion
    }
}

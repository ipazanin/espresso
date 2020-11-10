using System.Collections.Generic;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles
{
    public record GetCategoryArticlesQueryResponse
    {
        #region Properties
        public IEnumerable<IEnumerable<GetCategoryArticlesArticle>> Articles { get; init; } = new List<IEnumerable<GetCategoryArticlesArticle>>();

        public IEnumerable<GetCategoryArticlesNewsPortal> NewNewsPortals { get; init; } = new List<GetCategoryArticlesNewsPortal>();

        public int NewNewsPortalsPosition { get; init; }
        #endregion
    }
}

using System.Collections.Generic;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_2_0
{
    public record GetCategoryArticlesQueryResponse_2_0
    {
        #region Properties
        public IEnumerable<GetCategoryArticlesArticle_2_0> Articles { get; init; } = new List<GetCategoryArticlesArticle_2_0>();

        public IEnumerable<GetCategoryArticlesNewsPortal_2_0> NewNewsPortals { get; init; } = new List<GetCategoryArticlesNewsPortal_2_0>();

        public int NewNewsPortalsPosition { get; init; }
        #endregion
    }
}

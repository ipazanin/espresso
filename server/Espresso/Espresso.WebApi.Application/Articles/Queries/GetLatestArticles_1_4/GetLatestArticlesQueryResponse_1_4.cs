using System.Collections.Generic;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_4
{
    public record GetLatestArticlesQueryResponse_1_4
    {
        #region Properties
        public IEnumerable<GetLatestArticlesArticle_1_4> Articles { get; init; } = new List<GetLatestArticlesArticle_1_4>();

        public IEnumerable<GetLatestArticlesNewsPortal_1_4> NewNewsPortals { get; init; } = new List<GetLatestArticlesNewsPortal_1_4>();

        public int NewNewsPortalsPosition { get; init; }
        #endregion
    }
}

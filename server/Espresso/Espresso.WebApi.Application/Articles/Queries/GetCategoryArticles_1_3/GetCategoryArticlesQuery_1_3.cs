using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_1_3
{
    public record GetCategoryArticlesQuery_1_3 : Request<GetCategoryArticlesQueryResponse_1_3>
    {
        #region Properties
        public int Take { get; init; }

        public int Skip { get; init; }

        public int CategoryId { get; init; }

        public string? NewsPortalIds { get; init; }
        #endregion
    }
}

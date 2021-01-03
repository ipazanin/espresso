using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_3
{
    public record GetLatestArticlesQuery_1_3 : Request<GetLatestArticlesQueryResponse_1_3>
    {
        #region Properties
        public int Take { get; init; }

        public int Skip { get; init; }

        public string? NewsPortalIds { get; init; }

        public string? CategoryIds { get; init; }

        public string? TitleSearchQuery { get; init; }
        #endregion
    }
}

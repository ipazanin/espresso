using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_2_0
{
    public record GetCategoryArticlesQuery_2_0 : Request<GetCategoryArticlesQueryResponse_2_0>
    {
        #region Properties
        public int Take { get; init; }

        public int Skip { get; init; }

        public Guid? FirstArticleId { get; init; }

        public int CategoryId { get; init; }

        public int? RegionId { get; init; }

        public string? NewsPortalIds { get; init; }

        public int NewNewsPortalsPosition { get; init; }

        public string? TitleSearchQuery { get; init; }

        public TimeSpan MaxAgeOfNewNewsPortal { get; init; }
        #endregion
    }
}

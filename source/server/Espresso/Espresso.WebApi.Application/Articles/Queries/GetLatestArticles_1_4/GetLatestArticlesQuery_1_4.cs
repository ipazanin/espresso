using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_4
{
    public record GetLatestArticlesQuery_1_4 : Request<GetLatestArticlesQueryResponse_1_4>
    {
        #region Properties
        public int Take { get; init; }

        public int Skip { get; init; }

        public Guid? FirstArticleId { get; init; }

        public string? NewsPortalIds { get; init; }

        public string? CategoryIds { get; init; }

        public int NewNewsPortalsPosition { get; init; }

        public string? TitleSearchQuery { get; init; }

        public TimeSpan MaxAgeOfNewNewsPortal { get; init; }
        #endregion
    }
}

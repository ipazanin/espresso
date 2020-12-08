using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles
{
    public record GetLatestArticlesQuery : Request<GetLatestArticlesQueryResponse>
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

        public TimeSpan MaxAgeOfTrendingArticle { get; init; }

        public int FeaturedArticlesTake { get; init; }

        public TimeSpan MaxAgeOfFeaturedArticle { get; init; }
        #endregion
    }
}

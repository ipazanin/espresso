// GetLatestArticlesQuery_2_0.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;
using System;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_2_0
{
    public record GetLatestArticlesQuery_2_0 : Request<GetLatestArticlesQueryResponse_2_0>
    {
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

        public string? KeyWordsToFilterOut { get; init; }
    }
}

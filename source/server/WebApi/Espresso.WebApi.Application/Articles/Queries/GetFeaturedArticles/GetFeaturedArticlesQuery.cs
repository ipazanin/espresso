// GetFeaturedArticlesQuery.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;
using System;

namespace Espresso.WebApi.Application.Articles.Queries.GetFeaturedArticles
{
    public record GetFeaturedArticlesQuery : Request<GetFeaturedArticlesQueryResponse>
    {
        public int Take { get; init; }

        public int Skip { get; init; }
        public Guid? FirstArticleId { get; init; }
        public string? NewsPortalIds { get; init; } = string.Empty;
        public string? CategoryIds { get; init; } = string.Empty;

        public TimeSpan MaxAgeOfFeaturedArticle { get; init; }

        public TimeSpan MaxAgeOfTrendingArticle { get; init; }

        public string? KeyWordsToFilterOut { get; init; }
    }
}

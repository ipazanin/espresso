// GetTrendingArticlesQuery.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;
using System;

namespace Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles
{
    public record GetTrendingArticlesQuery : Request<GetTrendingArticlesQueryResponse>
    {
        public int Take { get; init; }
        public int Skip { get; init; }
        public Guid? FirstArticleId { get; init; }
        public TimeSpan MaxAgeOfTrendingArticle { get; init; }
    }
}

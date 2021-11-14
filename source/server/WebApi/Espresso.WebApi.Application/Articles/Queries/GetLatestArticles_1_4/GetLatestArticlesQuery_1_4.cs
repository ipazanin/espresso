// GetLatestArticlesQuery_1_4.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;
using System;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_4
{
#pragma warning disable S101 // Types should be named in PascalCase
    public record GetLatestArticlesQuery_1_4 : Request<GetLatestArticlesQueryResponse_1_4>
#pragma warning restore S101 // Types should be named in PascalCase
    {
        public int Take { get; init; }

        public int Skip { get; init; }

        public Guid? FirstArticleId { get; init; }

        public string? NewsPortalIds { get; init; }

        public string? CategoryIds { get; init; }

        public string? TitleSearchQuery { get; init; }
    }
}

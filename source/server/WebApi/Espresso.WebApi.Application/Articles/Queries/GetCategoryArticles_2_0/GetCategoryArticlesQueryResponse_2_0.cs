// GetCategoryArticlesQueryResponse_2_0.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Collections.Generic;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_2_0
{
#pragma warning disable S101 // Types should be named in PascalCase
    public record GetCategoryArticlesQueryResponse_2_0
#pragma warning restore S101 // Types should be named in PascalCase
    {
        public IEnumerable<GetCategoryArticlesArticle_2_0> Articles { get; init; } = new List<GetCategoryArticlesArticle_2_0>();

        public IEnumerable<GetCategoryArticlesNewsPortal_2_0> NewNewsPortals { get; init; } = new List<GetCategoryArticlesNewsPortal_2_0>();

        public int NewNewsPortalsPosition { get; init; }
    }
}

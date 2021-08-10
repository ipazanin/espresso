// GetLatestArticlesQueryResponse_2_0.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Collections.Generic;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_2_0
{
    public record GetLatestArticlesQueryResponse_2_0
    {
        public IEnumerable<GetLatestArticlesArticle_2_0> Articles { get; init; } = new List<GetLatestArticlesArticle_2_0>();

        public IEnumerable<GetLatestArticlesArticle_2_0> FeaturedArticles { get; init; } = new List<GetLatestArticlesArticle_2_0>();

        public IEnumerable<GetLatestArticlesNewsPortal_2_0> NewNewsPortals { get; init; } = new List<GetLatestArticlesNewsPortal_2_0>();

        public int NewNewsPortalsPosition { get; init; }

        public string LastAddedNewsPortalDate { get; init; } = string.Empty;
    }
}

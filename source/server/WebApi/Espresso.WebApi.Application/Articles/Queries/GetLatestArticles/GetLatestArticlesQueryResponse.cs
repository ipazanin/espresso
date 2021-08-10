﻿// GetLatestArticlesQueryResponse.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Collections.Generic;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles
{
    public record GetLatestArticlesQueryResponse
    {
        public IEnumerable<IEnumerable<GetLatestArticlesArticle>> Articles { get; init; } = new List<IEnumerable<GetLatestArticlesArticle>>();

        public IEnumerable<GetLatestArticlesArticle> FeaturedArticles { get; init; } = new List<GetLatestArticlesArticle>();

        public IEnumerable<GetLatestArticlesNewsPortal> NewNewsPortals { get; init; } = new List<GetLatestArticlesNewsPortal>();

        public int NewNewsPortalsPosition { get; init; }

        public string LastAddedNewsPortalDate { get; init; } = string.Empty;
    }
}
// GetLatestArticlesQueryResponse_1_3.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Collections.Generic;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_3
{
    public record GetLatestArticlesQueryResponse_1_3
    {
        public IEnumerable<GetLatestArticlesArticle_1_3> Articles { get; init; } = new List<GetLatestArticlesArticle_1_3>();
    }
}

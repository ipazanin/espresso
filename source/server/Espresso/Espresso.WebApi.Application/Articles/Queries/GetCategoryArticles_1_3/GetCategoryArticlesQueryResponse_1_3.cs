// GetCategoryArticlesQueryResponse_1_3.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Collections.Generic;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_1_3
{
    public record GetCategoryArticlesQueryResponse_1_3
    {
        public IEnumerable<GetCategoryArticlesArticle_1_3> Articles { get; init; } = new List<GetCategoryArticlesArticle_1_3>();
    }
}

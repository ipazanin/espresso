// GetCategoryArticlesQueryResponse_1_3.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_1_3;
#pragma warning disable S101 // Types should be named in PascalCase
public record GetCategoryArticlesQueryResponse_1_3
#pragma warning restore S101 // Types should be named in PascalCase
{
    public IEnumerable<GetCategoryArticlesArticle_1_3> Articles { get; init; } = new List<GetCategoryArticlesArticle_1_3>();
}

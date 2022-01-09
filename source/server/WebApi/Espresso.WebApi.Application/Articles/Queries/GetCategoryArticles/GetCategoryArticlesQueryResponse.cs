// GetCategoryArticlesQueryResponse.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles;

public record GetCategoryArticlesQueryResponse
{
    public IEnumerable<GetCategoryArticlesArticle> Articles { get; init; } = new List<GetCategoryArticlesArticle>();

    public IEnumerable<GetCategoryArticlesNewsPortal> NewNewsPortals { get; init; } = new List<GetCategoryArticlesNewsPortal>();

    public int NewNewsPortalsPosition { get; init; }
}

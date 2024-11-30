// GetCategoryArticlesQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles;

public record GetCategoryArticlesQueryResponse
{
    public IEnumerable<GetCategoryArticlesArticle> Articles { get; init; } = [];

    public IEnumerable<GetCategoryArticlesNewsPortal> NewNewsPortals { get; init; } = [];

    public int NewNewsPortalsPosition { get; init; }
}

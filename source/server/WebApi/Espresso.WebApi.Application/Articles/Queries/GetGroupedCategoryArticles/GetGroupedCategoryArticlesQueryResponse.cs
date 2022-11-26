// GetGroupedCategoryArticlesQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Articles.Queries.GetGroupedCategoryArticles;

public record GetGroupedCategoryArticlesQueryResponse
{
    public IEnumerable<IEnumerable<GetGroupedCategoryArticlesArticle>> Articles { get; init; } = new List<IEnumerable<GetGroupedCategoryArticlesArticle>>();

    public IEnumerable<GetGroupedCategoryArticlesNewsPortal> NewNewsPortals { get; init; } = new List<GetGroupedCategoryArticlesNewsPortal>();

    public int NewNewsPortalsPosition { get; init; }
}

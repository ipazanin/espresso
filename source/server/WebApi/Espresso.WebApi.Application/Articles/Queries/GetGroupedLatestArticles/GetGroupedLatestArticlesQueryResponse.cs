// GetGroupedLatestArticlesQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Articles.Queries.GetGroupedLatestArticles;

public record GetGroupedLatestArticlesQueryResponse
{
    public IEnumerable<IEnumerable<GetGroupedLatestArticlesArticle>> Articles { get; init; } = [];

    public IEnumerable<GetGroupedLatestArticlesArticle> FeaturedArticles { get; init; } = [];

    public IEnumerable<GetGroupedLatestArticlesNewsPortal> NewNewsPortals { get; init; } = [];

    public int NewNewsPortalsPosition { get; init; }

    public string LastAddedNewsPortalDate { get; init; } = string.Empty;
}

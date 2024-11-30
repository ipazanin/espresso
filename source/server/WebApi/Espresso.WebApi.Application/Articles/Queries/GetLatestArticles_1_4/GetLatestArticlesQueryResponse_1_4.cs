// GetLatestArticlesQueryResponse_1_4.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_4;
#pragma warning disable S101 // Types should be named in PascalCase
public record GetLatestArticlesQueryResponse_1_4
#pragma warning restore S101 // Types should be named in PascalCase
{
    public IEnumerable<GetLatestArticlesArticle_1_4> Articles { get; init; } = [];

    public IEnumerable<GetLatestArticlesNewsPortal_1_4> NewNewsPortals { get; init; } = [];

    public int NewNewsPortalsPosition { get; init; }
}

// GetLatestArticlesQuery_1_3.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_3;
#pragma warning disable S101 // Types should be named in PascalCase
public record GetLatestArticlesQuery_1_3 : Request<GetLatestArticlesQueryResponse_1_3>
#pragma warning restore S101 // Types should be named in PascalCase
{
    public int Take { get; init; }

    public int Skip { get; init; }

    public string? NewsPortalIds { get; init; }

    public string? CategoryIds { get; init; }

    public string? TitleSearchQuery { get; init; }
}

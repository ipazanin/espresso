// GetLatestArticlesQuery_2_0.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_2_0;
#pragma warning disable S101 // Types should be named in PascalCase
public record GetLatestArticlesQuery_2_0 : Request<GetLatestArticlesQueryResponse_2_0>
#pragma warning restore S101 // Types should be named in PascalCase
{
    public int Take { get; init; }

    public int Skip { get; init; }

    public Guid? FirstArticleId { get; init; }

    public string? NewsPortalIds { get; init; }

    public string? CategoryIds { get; init; }

    public string? TitleSearchQuery { get; init; }

    public string? KeyWordsToFilterOut { get; init; }
}

// GetGroupedLatestArticlesQuery.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application.Articles.Queries.GetGroupedLatestArticles;

public record GetGroupedLatestArticlesQuery : Request<GetGroupedLatestArticlesQueryResponse>
{
    public int Take { get; init; }

    public int Skip { get; init; }

    public Guid? FirstArticleId { get; init; }

    public string? NewsPortalIds { get; init; }

    public string? CategoryIds { get; init; }

    public string? TitleSearchQuery { get; init; }

    public string? KeyWordsToFilterOut { get; init; }
}

// GetGroupedCategoryArticlesQuery.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application.Articles.Queries.GetGroupedCategoryArticles;

public record GetGroupedCategoryArticlesQuery : Request<GetGroupedCategoryArticlesQueryResponse>
{
    public int Take { get; init; }

    public int Skip { get; init; }

    public Guid? FirstArticleId { get; init; }

    public int CategoryId { get; init; }

    public int? RegionId { get; init; }

    public string? NewsPortalIds { get; init; }

    public string? TitleSearchQuery { get; init; }

    public string? KeyWordsToFilterOut { get; init; }
}

// GetTrendingArticlesQuery.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles;

public record GetTrendingArticlesQuery : Request<GetTrendingArticlesQueryResponse>
{
    public required int Take { get; init; }
    public required int Skip { get; init; }
    public required Guid? FirstArticleId { get; init; }
    public required int? CategoryId { get; init; }
    public required int? MaxAgeOfTrendingArticleInHours { get; init; }
}

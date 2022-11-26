// GetTrendingArticlesQuery.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles;

public record GetTrendingArticlesQuery : Request<GetTrendingArticlesQueryResponse>
{
    public int Take { get; init; }
    public int Skip { get; init; }
    public Guid? FirstArticleId { get; init; }
}

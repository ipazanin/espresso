// GetCategoryArticlesQuery_1_3.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_1_3;
#pragma warning disable S101 // Types should be named in PascalCase
public record GetCategoryArticlesQuery_1_3 : Request<GetCategoryArticlesQueryResponse_1_3>
#pragma warning restore S101 // Types should be named in PascalCase
{
    public int Take { get; init; }

    public int Skip { get; init; }

    public int CategoryId { get; init; }

    public string? NewsPortalIds { get; init; }
}

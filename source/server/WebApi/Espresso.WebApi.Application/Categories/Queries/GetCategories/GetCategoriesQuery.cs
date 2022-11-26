// GetCategoriesQuery.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application.Categories.Queries.GetCategories;

public record GetCategoriesQuery : Request<GetCategoriesQueryResponse>
{
}

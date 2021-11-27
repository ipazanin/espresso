// GetCategoriesQueryResponse.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Categories.Queries.GetCategories;

public record GetCategoriesQueryResponse
{
    public IEnumerable<GetCategoriesCategory> Categories { get; init; } = new List<GetCategoriesCategory>();
}

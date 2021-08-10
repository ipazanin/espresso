// GetCategoriesQueryResponse.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Collections.Generic;

namespace Espresso.WebApi.Application.Categories.Queries.GetCategories
{
    public record GetCategoriesQueryResponse
    {
        public IEnumerable<GetCategoriesCategory> Categories { get; init; } = new List<GetCategoriesCategory>();
    }
}

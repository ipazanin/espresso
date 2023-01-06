// GetCategoryDetailsQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CategoryDataTransferObjects;

namespace Espresso.Dashboard.Application.Categories.Queries.GetCategoryDetails;

public sealed class GetCategoryDetailsQueryResponse
{
    public GetCategoryDetailsQueryResponse(CategoryDto category)
    {
        Category = category;
    }

    public CategoryDto Category { get; }
}

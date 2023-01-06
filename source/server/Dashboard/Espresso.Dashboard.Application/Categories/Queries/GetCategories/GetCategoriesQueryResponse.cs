// GetCategoriesQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CategoryDataTransferObjects;
using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;

namespace Espresso.Dashboard.Application.Categories.Queries.GetCategories;

public class GetCategoriesQueryResponse
{
    public GetCategoriesQueryResponse(PagedList<CategoryDto> categoriesPagedList)
    {
        CategoriesPagedList = categoriesPagedList;
    }

    public PagedList<CategoryDto> CategoriesPagedList { get; }
}

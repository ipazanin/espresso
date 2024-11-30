// CategoriesListBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CategoryDataTransferObjects;
using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Dashboard.Application.Categories.Commands.DeleteCategory;
using Espresso.Dashboard.Application.Categories.Queries.GetCategories;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Espresso.Dashboard.Pages.CategoryPages.CategoriesList;

/// <summary>
/// NewsPortalDetailsBase.
/// </summary>
public class CategoriesListBase : ComponentBase
{
    protected MudTable<CategoryDto> Table { get; set; } = null!;

    [Inject]
    private ISender Sender { get; init; } = null!;

    [Inject]
    private IDialogService DialogService { get; init; } = null!;

    protected async Task<TableData<CategoryDto>> GetTableData(TableState tableState, CancellationToken cancellationToken)
    {
        var pagingParameters = new PagingParameters
        {
            CurrentPage = tableState.Page + 1,
            PageSize = tableState.PageSize,
        };

        var request = new GetCategoriesQuery(pagingParameters: pagingParameters);
        var getArticlesResponse = await Sender.Send(request, cancellationToken);

        var tableData = new TableData<CategoryDto>
        {
            Items = getArticlesResponse.CategoriesPagedList.Items,
            TotalItems = getArticlesResponse.CategoriesPagedList.PagingMetadata.TotalCount,
        };

        return tableData;
    }

    protected async Task DeleteCategory(int categoryId)
    {
        var shouldBeDeleted = await DialogService.ShowMessageBox(
            title: "Delete Category",
            message: "Are you sure you want to delete Category?",
            yesText: "Yes",
            noText: "No",
            options: new DialogOptions
            {
                CloseOnEscapeKey = true,
                CloseButton = false,
            });

        if (shouldBeDeleted != true)
        {
            return;
        }

        var request = new DeleteCategoryCommand(categoryId);
        await Sender.Send(request);

        await Table.ReloadServerData();
    }
}

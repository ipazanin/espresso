// RegionsListBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Dashboard.Application.Regions.Commands.DeleteRegion;
using Espresso.Dashboard.Application.Regions.Queries.GetRegions;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Espresso.Dashboard.Pages.RegionPages.RegionsList;

/// <summary>
/// NewsPortalDetailsBase.
/// </summary>
public class RegionsListBase : ComponentBase
{
    protected MudTable<RegionDto> Table { get; set; } = null!;

    [Inject]
    private ISender Sender { get; init; } = null!;

    [Inject]
    private IDialogService DialogService { get; init; } = null!;

    protected async Task<TableData<RegionDto>> GetTableData(TableState tableState, CancellationToken cancellationToken)
    {
        var pagingParameters = new PagingParameters
        {
            CurrentPage = tableState.Page + 1,
            PageSize = tableState.PageSize,
        };

        var request = new GetRegionsQuery(pagingParameters: pagingParameters);
        var response = await Sender.Send(request, cancellationToken);

        var tableData = new TableData<RegionDto>
        {
            Items = response.RegionsPagedList.Items,
            TotalItems = response.RegionsPagedList.PagingMetadata.TotalCount,
        };

        return tableData;
    }

    protected async Task DeleteRegion(int categoryId)
    {
        var shouldBeDeleted = await DialogService.ShowMessageBox(
            title: "Delete Region",
            message: "Are you sure you want to delete Region?",
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

        var request = new DeleteRegionCommand(categoryId);
        await Sender.Send(request);

        await Table.ReloadServerData();
    }
}

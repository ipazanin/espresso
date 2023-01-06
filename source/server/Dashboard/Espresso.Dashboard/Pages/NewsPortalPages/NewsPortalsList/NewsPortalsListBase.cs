// NewsPortalsListBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Common.Constants;
using Espresso.Dashboard.Application.NewsPortals.Commands.DeleteNewsPortal;
using Espresso.Dashboard.Application.NewsPortals.Queries.GetNewsPortals;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Espresso.Dashboard.Pages.NewsPortalPages.NewsPortalsList;

/// <summary>
/// Base class for News Portal List Component.
/// </summary>
[Authorize(Roles = RoleConstants.AdminRoleName)]
public class NewsPortalsListBase : ComponentBase
{
    protected MudTable<GetNewsPortalsQueryNewsPortal> Table { get; set; } = null!;

    protected string SearchString { get; set; } = string.Empty;

    [Inject]
    private ISender Sender { get; init; } = null!;

    [Inject]
    private IDialogService DialogService { get; init; } = null!;

    protected async Task<TableData<GetNewsPortalsQueryNewsPortal>> GetTableData(TableState tableState)
    {
        var pagingParameters = new PagingParameters
        {
            CurrentPage = tableState.Page + 1,
            PageSize = tableState.PageSize,
            OrderType = tableState.SortDirection switch
            {
                SortDirection.Ascending => OrderType.Ascending,
                _ => OrderType.Descending,
            },
            SortColumn = tableState.SortLabel,
            SearchString = SearchString,
        };

        var request = new GetNewsPortalsQuery(pagingParameters: pagingParameters);
        var getArticlesResponse = await Sender.Send(request);

        var tableData = new TableData<GetNewsPortalsQueryNewsPortal>
        {
            Items = getArticlesResponse.NewsPortals.Items,
            TotalItems = getArticlesResponse.NewsPortals.PagingMetadata.TotalCount,
        };

        return tableData;
    }

    /// <summary>
    /// Deletes <see cref="NewsPortal"/>.
    /// </summary>
    /// <param name="newsPortalId">Id of <see cref="NewsPortal"/> to delete.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    protected async Task DeleteNewsPortal(int newsPortalId)
    {
        var shouldBeDeleted = await DialogService.ShowMessageBox(
            title: "Delete Source",
            message: "Are you sure you want to delete Source?",
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

        await Sender.Send(new DeleteNewsPortalCommand(newsPortalId));

        await Table.ReloadServerData();
    }

    protected void OnSearch(string text)
    {
        SearchString = text;
        _ = Table.ReloadServerData();
    }
}

// RssFeedsListBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Dashboard.Application.RssFeeds.Commands.DeleteRssFeed;
using Espresso.Dashboard.Application.RssFeeds.Queries.GetRssFeeds;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Espresso.Dashboard.Pages.RssFeedPages.RssFeedList;

/// <summary>
/// NewsPortalDetailsBase.
/// </summary>
public class RssFeedsListBase : ComponentBase
{
    protected MudTable<GetRssFeedsQueryRssFeed> Table { get; set; } = null!;

    protected string SearchString { get; set; } = string.Empty;

    [Inject]
    private ISender Sender { get; init; } = null!;

    [Inject]
    private IDialogService DialogService { get; init; } = null!;

    protected async Task<TableData<GetRssFeedsQueryRssFeed>> GetTableData(TableState tableState, CancellationToken cancellationToken)
    {
        var pagingParameters = new PagingParameters
        {
            CurrentPage = tableState.Page + 1,
            PageSize = tableState.PageSize,
            SearchString = SearchString,
            SortColumn = tableState.SortLabel ?? string.Empty,
            OrderType = tableState.SortDirection switch
            {
                SortDirection.Ascending => OrderType.Ascending,
                SortDirection.None => OrderType.Descending,
                SortDirection.Descending => OrderType.Descending,
                _ => OrderType.Descending,
            },
        };

        var request = new GetRssFeedsQuery(pagingParameters: pagingParameters);
        var response = await Sender.Send(request, cancellationToken);

        var tableData = new TableData<GetRssFeedsQueryRssFeed>
        {
            Items = response.RssFeeds.Items,
            TotalItems = response.RssFeeds.PagingMetadata.TotalCount,
        };

        return tableData;
    }

    protected async Task DeleteRssFeed(int rssFeedId)
    {
        var shouldRssFeedBeDeleted = await DialogService.ShowMessageBox(
            title: "Delete RSS Feed",
            message: "Are you sure you want to delete RSS Feed?",
            yesText: "Yes",
            noText: "No",
            options: new DialogOptions
            {
                CloseOnEscapeKey = true,
                CloseButton = false,
            });

        if (shouldRssFeedBeDeleted != true)
        {
            return;
        }

        await Sender.Send(new DeleteRssFeedCommand(rssFeedId));

        await Table.ReloadServerData();
    }

    protected void OnSearch(string text)
    {
        SearchString = text;
        _ = Table.ReloadServerData();
    }
}

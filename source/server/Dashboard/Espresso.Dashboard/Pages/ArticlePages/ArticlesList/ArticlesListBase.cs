// ArticlesListBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Dashboard.Application.Articles.Queries.GetArticles;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Espresso.Dashboard.Pages.ArticlePages.ArticlesList;

/// <summary>
/// NewsPortalDetailsBase.
/// </summary>
public class ArticlesListBase : ComponentBase
{
    protected string SearchString { get; set; } = string.Empty;

    protected MudTable<GetArticlesArticle> Table { get; set; } = null!;

    [Inject]
    private ISender Sender { get; init; } = null!;

    protected async Task<TableData<GetArticlesArticle>> GetTableData(TableState tableState)
    {
        var pagingParameters = new PagingParameters
        {
            CurrentPage = tableState.Page + 1,
            PageSize = tableState.PageSize,
            SearchString = SearchString,
            SortColumn = tableState.SortDirection is SortDirection.None ? nameof(GetArticlesArticle.Created) : tableState.SortLabel,
            OrderType = tableState.SortDirection switch
            {
                SortDirection.Ascending => OrderType.Ascending,
                _ => OrderType.Descending,
            },
        };

        var request = new GetArticlesQuery(pagingParameters: pagingParameters);
        var getArticlesResponse = await Sender.Send(request);

        var tableData = new TableData<GetArticlesArticle>
        {
            Items = getArticlesResponse.Articles.Items,
            TotalItems = getArticlesResponse.Articles.PagingMetadata.TotalCount,
        };

        return tableData;
    }

    protected void OnSearch(string text)
    {
        SearchString = text;
        _ = Table.ReloadServerData();
    }
}

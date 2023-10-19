// CountriesListBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CountryDataTransferObjects;
using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Common.Constants;
using Espresso.Dashboard.Application.Countries.Commands.DeleteCountry;
using Espresso.Dashboard.Application.Countries.Queries.GetCountries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Espresso.Dashboard.Pages.CountryPages.CountriesList;

/// <summary>
/// Base class for News Portal List Component.
/// </summary>
[Authorize(Roles = RoleConstants.AdminRoleName)]
public class CountriesListBase : ComponentBase
{
    protected MudTable<CountryDto> Table { get; set; } = null!;

    protected string SearchString { get; set; } = string.Empty;

    [Inject]
    private ISender Sender { get; init; } = null!;

    [Inject]
    private IDialogService DialogService { get; init; } = null!;

    protected async Task<TableData<CountryDto>> GetTableData(TableState tableState)
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

        var request = new GetCountriesQuery(pagingParameters: pagingParameters);
        var getCountriesResponse = await Sender.Send(request);

        var tableData = new TableData<CountryDto>
        {
            Items = getCountriesResponse.CountriesPagedList.Items,
            TotalItems = getCountriesResponse.CountriesPagedList.PagingMetadata.TotalCount,
        };

        return tableData;
    }

    protected async Task DeleteCountry(int countryId, string countryName)
    {
        var shouldBeDeleted = await DialogService.ShowMessageBox(
            title: "Delete Country",
            message: $"Are you sure you want to delete {countryName}?",
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

        await Sender.Send(new DeleteCountryCommand(countryId));

        await Table.ReloadServerData();
    }

    protected void OnSearch(string text)
    {
        SearchString = text;
        _ = Table.ReloadServerData();
    }
}

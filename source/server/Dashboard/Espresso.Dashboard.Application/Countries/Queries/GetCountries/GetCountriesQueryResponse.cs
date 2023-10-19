// GetCountriesQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CountryDataTransferObjects;
using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;

namespace Espresso.Dashboard.Application.Countries.Queries.GetCountries;

public class GetCountriesQueryResponse
{
    public GetCountriesQueryResponse(PagedList<CountryDto> countriesPagedList)
    {
        CountriesPagedList = countriesPagedList;
    }

    public PagedList<CountryDto> CountriesPagedList { get; }
}

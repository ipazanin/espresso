// GetCountryDetailsQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CountryDataTransferObjects;

namespace Espresso.Dashboard.Application.Countries.Queries.GetCountryDetails;

public sealed class GetCountryDetailsQueryResponse
{
    public GetCountryDetailsQueryResponse(CountryDto? country)
    {
        Country = country;
    }

    public CountryDto? Country { get; }
}

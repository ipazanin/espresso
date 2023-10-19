// GetCountryImageQuery.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CountryDataTransferObjects;

namespace Espresso.Dashboard.Application.Countries.Queries.GetCountryImage;

public sealed class GetCountryImageQueryResponse
{
    public GetCountryImageQueryResponse(CountryImageDto countryImage)
    {
        CountryImage = countryImage;
    }

    public CountryImageDto CountryImage { get; }
}

// GetCountryDetailsQuery.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CountryDataTransferObjects;
using MediatR;

namespace Espresso.Dashboard.Application.Countries.Queries.GetCountryDetails;

public sealed class GetCountryDetailsQuery : IRequest<GetCountryDetailsQueryResponse>
{
    public GetCountryDetailsQuery(int countryId)
    {
        CountryId = countryId;
    }

    public int CountryId { get; }
}

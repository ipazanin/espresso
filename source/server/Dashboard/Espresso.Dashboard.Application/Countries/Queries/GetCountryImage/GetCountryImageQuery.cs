// GetCountryImageQuery.cs
//
// © 2022 Espresso News. All rights reserved.

using MediatR;

namespace Espresso.Dashboard.Application.Countries.Queries.GetCountryImage;

public sealed class GetCountryImageQuery : IRequest<GetCountryImageQueryResponse>
{
    public GetCountryImageQuery(int countryId)
    {
        CountryId = countryId;
    }

    public int CountryId { get; }
}

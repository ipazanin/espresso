// GetCountryDetailsQueryHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CountryDataTransferObjects;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.Countries.Queries.GetCountryDetails;

public sealed class GetCountryDetailsQueryHandler : IRequestHandler<GetCountryDetailsQuery, GetCountryDetailsQueryResponse>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;

    public GetCountryDetailsQueryHandler(IEspressoDatabaseContext espressoDatabaseContext)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
    }

    public async Task<GetCountryDetailsQueryResponse> Handle(GetCountryDetailsQuery request, CancellationToken cancellationToken)
    {
        var country = await _espressoDatabaseContext
            .Countries
            .AsNoTracking()
            .Select(CountryDto.GetProjection())
            .FirstOrDefaultAsync(country => country.Id == request.CountryId, cancellationToken);

        return new(country);
    }
}

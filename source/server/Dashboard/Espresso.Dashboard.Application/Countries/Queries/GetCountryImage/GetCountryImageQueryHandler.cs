// GetCountryImageQueryHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CountryDataTransferObjects;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.Countries.Queries.GetCountryImage;

public sealed class GetCountryImageQueryHandler : IRequestHandler<GetCountryImageQuery, GetCountryImageQueryResponse>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;

    public GetCountryImageQueryHandler(IEspressoDatabaseContext espressoDatabaseContext)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
    }

    public async Task<GetCountryImageQueryResponse> Handle(GetCountryImageQuery request, CancellationToken cancellationToken)
    {
        var countryImage = await _espressoDatabaseContext
            .CountryImages
            .FirstOrDefaultAsync(countryImage => countryImage.CountryId == request.CountryId, cancellationToken);

        var countryImageDto = CountryImageDto.GetProjection().Compile().Invoke(countryImage);

        return new GetCountryImageQueryResponse(countryImageDto);
    }
}

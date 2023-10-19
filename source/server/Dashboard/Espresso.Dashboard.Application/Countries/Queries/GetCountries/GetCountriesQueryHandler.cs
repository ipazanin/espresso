// GetCountriesQueryHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CountryDataTransferObjects;
using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.Countries.Queries.GetCountries;

public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, GetCountriesQueryResponse>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;

    public GetCountriesQueryHandler(IEspressoDatabaseContext espressoDatabaseContext)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
    }

    public async Task<GetCountriesQueryResponse> Handle(
        GetCountriesQuery request,
        CancellationToken cancellationToken)
    {
        var countries = await _espressoDatabaseContext
            .Countries
            .AsNoTracking()
            .FilterCountries(request.PagingParameters)
            .OrderCountries(request.PagingParameters)
            .Skip(request.PagingParameters.GetSkip())
            .Take(request.PagingParameters.GetTake())
            .Select(CountryDto.GetProjection())
            .ToArrayAsync(cancellationToken);

        var countriesCount = await _espressoDatabaseContext
            .Countries
            .AsNoTracking()
            .FilterCountries(request.PagingParameters)
            .OrderCountries(request.PagingParameters)
            .Skip(request.PagingParameters.GetSkip())
            .Take(request.PagingParameters.GetTake())
            .CountAsync(cancellationToken);

        var pagingMetadata = new PagingMetadata(
            currentPage: request.PagingParameters.CurrentPage,
            pageSize: request.PagingParameters.PageSize,
            totalCount: countriesCount);

        var countriesPagedList = new PagedList<CountryDto>(countries, pagingMetadata);

        return new GetCountriesQueryResponse(countriesPagedList);
    }
}

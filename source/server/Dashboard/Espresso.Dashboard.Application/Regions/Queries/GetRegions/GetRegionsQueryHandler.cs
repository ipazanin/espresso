// GetRegionsQueryHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.Regions.Queries.GetRegions;

public class GetRegionsQueryHandler : IRequestHandler<GetRegionsQuery, GetRegionsQueryResponse>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;

    public GetRegionsQueryHandler(IEspressoDatabaseContext espressoDatabaseContext)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
    }

    public async Task<GetRegionsQueryResponse> Handle(GetRegionsQuery request, CancellationToken cancellationToken)
    {
        var regions = await _espressoDatabaseContext
            .Regions
            .AsNoTracking()
            .AsSplitQuery()
            .Skip(request.PagingParameters.GetSkip())
            .Take(request.PagingParameters.GetTake())
            .Select(RegionDto.Projection)
            .ToArrayAsync(cancellationToken);

        var regionsCount = await _espressoDatabaseContext
            .Regions
            .AsNoTracking()
            .AsSplitQuery()
            .CountAsync(cancellationToken);

        var pagingMetadata = new PagingMetadata(
            currentPage: request.PagingParameters.CurrentPage,
            pageSize: request.PagingParameters.PageSize,
            totalCount: regionsCount);

        var pagedList = new PagedList<RegionDto>(
            items: regions,
            pagingMetadata: pagingMetadata);

        return new GetRegionsQueryResponse(pagedList);
    }
}

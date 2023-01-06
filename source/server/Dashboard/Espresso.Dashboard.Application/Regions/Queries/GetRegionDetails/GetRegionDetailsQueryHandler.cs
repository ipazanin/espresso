// GetRegionDetailsQueryHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.Regions.Queries.GetRegionDetails;

public class GetRegionDetailsQueryHandler : IRequestHandler<GetRegionDetailsQuery, GetRegionDetailsQueryResponse>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;

    public GetRegionDetailsQueryHandler(IEspressoDatabaseContext espressoDatabaseContext)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
    }

    public async Task<GetRegionDetailsQueryResponse> Handle(GetRegionDetailsQuery request, CancellationToken cancellationToken)
    {
        var region = await _espressoDatabaseContext
            .Regions
            .AsNoTracking()
            .AsSplitQuery()
            .Select(RegionDto.Projection)
            .FirstAsync(region => region.Id == request.RegionId, cancellationToken);

        var newsPortals = await _espressoDatabaseContext
            .NewsPortals
            .AsNoTracking()
            .AsSplitQuery()
            .Where(newsPortal => newsPortal.RegionId == request.RegionId)
            .Select(NewsPortalDto.GetProjection())
            .ToArrayAsync(cancellationToken);

        return new GetRegionDetailsQueryResponse(region, newsPortals);
    }
}

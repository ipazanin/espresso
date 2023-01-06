// GetRegionDetailsQuery.cs
//
// © 2022 Espresso News. All rights reserved.

using MediatR;

namespace Espresso.Dashboard.Application.Regions.Queries.GetRegionDetails;

public class GetRegionDetailsQuery : IRequest<GetRegionDetailsQueryResponse>
{
    public GetRegionDetailsQuery(int regionId)
    {
        RegionId = regionId;
    }

    public int RegionId { get; }
}

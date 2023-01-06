// GetRegionDetailsQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;

namespace Espresso.Dashboard.Application.Regions.Queries.GetRegionDetails;

public class GetRegionDetailsQueryResponse
{
    public GetRegionDetailsQueryResponse(RegionDto region, IEnumerable<NewsPortalDto> newsPortals)
    {
        Region = region;
        NewsPortals = newsPortals;
    }

    public RegionDto Region { get; }

    public IEnumerable<NewsPortalDto> NewsPortals { get; }
}

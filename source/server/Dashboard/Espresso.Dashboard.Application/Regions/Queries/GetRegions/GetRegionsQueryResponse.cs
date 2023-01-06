// GetRegionsQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;

namespace Espresso.Dashboard.Application.Regions.Queries.GetRegions;

public class GetRegionsQueryResponse
{
    public GetRegionsQueryResponse(PagedList<RegionDto> regionsPagedList)
    {
        RegionsPagedList = regionsPagedList;
    }

    public PagedList<RegionDto> RegionsPagedList { get; }
}

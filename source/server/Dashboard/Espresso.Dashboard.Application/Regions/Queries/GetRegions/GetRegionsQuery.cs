// GetRegionsQuery.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using MediatR;

namespace Espresso.Dashboard.Application.Regions.Queries.GetRegions;

public class GetRegionsQuery : IRequest<GetRegionsQueryResponse>
{
    public GetRegionsQuery(PagingParameters pagingParameters)
    {
        PagingParameters = pagingParameters;
    }

    public PagingParameters PagingParameters { get; }
}

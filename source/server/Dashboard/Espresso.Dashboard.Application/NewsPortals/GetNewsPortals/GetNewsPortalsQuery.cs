// GetNewsPortalsQuery.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using MediatR;

namespace Espresso.Dashboard.Application.NewsPortals.GetNewsPortals;

public class GetNewsPortalsQuery : IRequest<GetNewsPortalsQueryResponse>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetNewsPortalsQuery"/> class.
    /// </summary>
    /// <param name="pagingParameters"></param>
    public GetNewsPortalsQuery(PagingParameters pagingParameters)
    {
        PagingParameters = pagingParameters;
    }

    public PagingParameters PagingParameters { get; }
}

// GetNewsPortalsQuery.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using MediatR;

namespace Espresso.Dashboard.Application.NewsPortals.GetNewsPortals;

public class GetNewsPortalsQuery : IRequest<GetNewsPortalsQueryResponse>
{
    public PagingParameters PagingParameters { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetNewsPortalsQuery"/> class.
    /// </summary>
    /// <param name="pagingParameters"></param>
#pragma warning disable SA1201 // Elements should appear in the correct order
    public GetNewsPortalsQuery(PagingParameters pagingParameters)
#pragma warning restore SA1201 // Elements should appear in the correct order
    {
        PagingParameters = pagingParameters;
    }
}

// GetNewsPortalsQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;

namespace Espresso.Dashboard.Application.NewsPortals.GetNewsPortals;

public class GetNewsPortalsQueryResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetNewsPortalsQueryResponse"/> class.
    /// </summary>
    /// <param name="newsPortals"></param>
    public GetNewsPortalsQueryResponse(PagedList<GetNewsPortalsNewsPortal> newsPortals)
    {
        NewsPortals = newsPortals;
    }

    public PagedList<GetNewsPortalsNewsPortal> NewsPortals { get; }
}

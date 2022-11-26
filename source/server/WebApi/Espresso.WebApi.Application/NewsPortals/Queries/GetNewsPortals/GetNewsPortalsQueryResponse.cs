// GetNewsPortalsQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals;

public class GetNewsPortalsQueryResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetNewsPortalsQueryResponse"/> class.
    /// </summary>
    /// <param name="newsPortals"></param>
    public GetNewsPortalsQueryResponse(IEnumerable<GetNewsPortalsNewsPortal> newsPortals)
    {
        NewsPortals = newsPortals;
    }

    public IEnumerable<GetNewsPortalsNewsPortal> NewsPortals { get; }

    public override string ToString()
    {
        return $"{nameof(NewsPortals)}:{NewsPortals.Count()}";
    }
}

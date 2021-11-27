// GetNewsPortalsQueryResponse_1_3.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals_1_3;
#pragma warning disable S101 // Types should be named in PascalCase
public class GetNewsPortalsQueryResponse_1_3
#pragma warning restore S101 // Types should be named in PascalCase
{
    public IEnumerable<GetNewsPortalsNewsPortal_1_3> NewsPortals { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetNewsPortalsQueryResponse_1_3"/> class.
    /// </summary>
    /// <param name="newsPortals"></param>
#pragma warning disable SA1201 // Elements should appear in the correct order
    public GetNewsPortalsQueryResponse_1_3(IEnumerable<GetNewsPortalsNewsPortal_1_3> newsPortals)
#pragma warning restore SA1201 // Elements should appear in the correct order
    {
        NewsPortals = newsPortals;
    }

    public override string ToString()
    {
        return $"{nameof(NewsPortals)}:{NewsPortals.Count()}";
    }
}

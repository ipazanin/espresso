// GetNewsPortalsQueryResponse.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Collections.Generic;
using System.Linq;

namespace Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals
{
    public class GetNewsPortalsQueryResponse
    {
        public IEnumerable<GetNewsPortalsNewsPortal> NewsPortals { get; } = new List<GetNewsPortalsNewsPortal>();

        /// <summary>
        /// Initializes a new instance of the <see cref="GetNewsPortalsQueryResponse"/> class.
        /// </summary>
        /// <param name="newsPortals"></param>
        public GetNewsPortalsQueryResponse(IEnumerable<GetNewsPortalsNewsPortal> newsPortals)
        {
            NewsPortals = newsPortals;
        }

        public override string ToString()
        {
            return $"{nameof(NewsPortals)}:{NewsPortals.Count()}";
        }
    }
}

// GetNewsPortalsQueryResponse.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Collections.Generic;
using System.Linq;

namespace Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals
{
    public class GetNewsPortalsQueryResponse
    {
        public IEnumerable<GetNewsPortalsNewsPortal> NewsPortals { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetNewsPortalsQueryResponse"/> class.
        /// </summary>
        /// <param name="newsPortals"></param>
#pragma warning disable SA1201 // Elements should appear in the correct order
        public GetNewsPortalsQueryResponse(IEnumerable<GetNewsPortalsNewsPortal> newsPortals)
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
            NewsPortals = newsPortals;
        }

        public override string ToString()
        {
            return $"{nameof(NewsPortals)}:{NewsPortals.Count()}";
        }
    }
}

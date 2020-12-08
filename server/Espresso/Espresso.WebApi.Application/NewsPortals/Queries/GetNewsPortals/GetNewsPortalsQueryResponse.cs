using System.Collections.Generic;
using System.Linq;

namespace Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals
{
    public class GetNewsPortalsQueryResponse
    {
        public IEnumerable<GetNewsPortalsNewsPortal> NewsPortals { get; } = new List<GetNewsPortalsNewsPortal>();

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

using System.Collections.Generic;
using System.Linq;

namespace Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals_1_3
{
    public class GetNewsPortalsQueryResponse_1_3
    {
        public IEnumerable<GetNewsPortalsNewsPortal_1_3> NewsPortals { get; } = new List<GetNewsPortalsNewsPortal_1_3>();

        public GetNewsPortalsQueryResponse_1_3(IEnumerable<GetNewsPortalsNewsPortal_1_3> newsPortals)
        {
            NewsPortals = newsPortals;
        }

        public override string ToString()
        {
            return $"{nameof(NewsPortals)}:{NewsPortals.Count()}";
        }
    }
}

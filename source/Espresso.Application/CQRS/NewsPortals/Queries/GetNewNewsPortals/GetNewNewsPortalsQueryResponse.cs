
using System.Collections.Generic;
using System.Linq;
using Espresso.Application.CQRS.NewsPortals.Queries.GetNewsPortals;

namespace Espresso.Application.CQRS.NewsPortals.Queries.GetNewNewsportals
{
    public class GetNewNewsPortalsQueryResponse
    {
        public IEnumerable<NewsPortalViewModel> NewsPortals { get; }

        public int WidgetPosition { get; }

        public GetNewNewsPortalsQueryResponse(IEnumerable<NewsPortalViewModel> newsPortals)
        {
            NewsPortals = newsPortals;
            WidgetPosition = 3;
        }

        public override string ToString()
        {
            return $"{nameof(NewsPortals)}:{NewsPortals.Count()}, {nameof(WidgetPosition)}:{WidgetPosition}";
        }
    }
}

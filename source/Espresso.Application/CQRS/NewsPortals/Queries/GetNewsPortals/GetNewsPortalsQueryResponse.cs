using System.Collections.Generic;
using System.Linq;
using Espresso.Application.ViewModels.NewsPortalViewModels;

namespace Espresso.Application.CQRS.NewsPortals.Queries.GetNewsPortals
{
    public class GetNewsPortalsQueryResponse
    {
        public IEnumerable<NewsPortalViewModel> NewsPortals { get; } = new List<NewsPortalViewModel>();

        public GetNewsPortalsQueryResponse(IEnumerable<NewsPortalViewModel> newsPortals)
        {
            NewsPortals = newsPortals;
        }

        public override string ToString()
        {
            return $"{nameof(NewsPortals)}:{NewsPortals.Count()}";
        }
    }
}

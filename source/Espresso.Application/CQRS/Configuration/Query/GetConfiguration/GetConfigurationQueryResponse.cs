using System.Collections.Generic;
using System.Linq;
using Espresso.Application.CQRS.Categories.Queries.GetCategories;
using Espresso.Application.CQRS.NewsPortals.Queries.GetNewsPortals;

namespace Espresso.Application.CQRS.Configuration.Query.GetConfiguration
{
    public class GetConfigurationQueryResponse
    {
        public IEnumerable<CategoryViewModel> Categories { get; }

        public IEnumerable<NewsPortalViewModel> NewsPortals { get; }

        public GetConfigurationQueryResponse(IEnumerable<CategoryViewModel> categories, IEnumerable<NewsPortalViewModel> newsPortals)
        {
            Categories = categories;
            NewsPortals = newsPortals;
        }

        public override string ToString()
        {
            return $"{nameof(Categories)}:{Categories.Count()}, {nameof(NewsPortals)}:{NewsPortals.Count()}";
        }
    }
}

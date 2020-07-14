using System.Collections.Generic;
using System.Linq;
using Espresso.Application.CQRS.Categories.Queries.GetCategories;

namespace Espresso.Application.CQRS.Configuration.Queries.GetConfiguration
{
    public class GetConfigurationQueryResponse
    {
        public IEnumerable<CategoryViewModel> Categories { get; }

        public IEnumerable<GetConfigurationQueryNewsPortalViewModel> NewsPortals { get; }

        public GetConfigurationQueryResponse(IEnumerable<CategoryViewModel> categories, IEnumerable<GetConfigurationQueryNewsPortalViewModel> newsPortals)
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

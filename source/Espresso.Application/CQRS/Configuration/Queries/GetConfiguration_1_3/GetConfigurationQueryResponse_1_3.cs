using System.Collections.Generic;
using System.Linq;

namespace Espresso.Application.CQRS.Configuration.Queries.GetConfiguration_1_3
{
    public class GetConfigurationQueryResponse_1_3
    {
        public IEnumerable<GetConfigurationQueryCategoryViewModel_1_3> Categories { get; }

        public IEnumerable<GetConfigurationQueryNewsPortalViewModel_1_3> NewsPortals { get; }

        public GetConfigurationQueryResponse_1_3(
            IEnumerable<GetConfigurationQueryCategoryViewModel_1_3> categories,
            IEnumerable<GetConfigurationQueryNewsPortalViewModel_1_3> newsPortals
        )
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

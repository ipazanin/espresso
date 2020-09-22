using System.Collections.Generic;
using System.Linq;

namespace Espresso.WebApi.Application.CQRS.Configuration.Queries.GetWebConfiguration
{
    public class GetWebConfigurationQueryResponse
    {
        public IEnumerable<GetWebConfigurationCategory> Categories { get; }
        public IEnumerable<int> NewsPortalIds { get; }

        public GetWebConfigurationQueryResponse(
            IEnumerable<GetWebConfigurationCategory> categories,
            IEnumerable<int> newsPortalIds
        )
        {
            Categories = categories;
            NewsPortalIds = newsPortalIds;
        }

        public override string ToString()
        {
            return $"{nameof(Categories)}:{Categories.Count()}, " +
                $"{nameof(NewsPortalIds)}:{NewsPortalIds.Count()}";
        }
    }
}

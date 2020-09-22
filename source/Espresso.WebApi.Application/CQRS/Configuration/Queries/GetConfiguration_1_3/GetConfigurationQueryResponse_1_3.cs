using System.Collections.Generic;
using System.Linq;

namespace Espresso.WebApi.Application.CQRS.Configuration.Queries.GetConfiguration_1_3
{
    public class GetConfigurationQueryResponse_1_3
    {
        public IEnumerable<GetConfigurationCategory_1_3> Categories { get; }

        public IEnumerable<GetConfigurationNewsPortal_1_3> NewsPortals { get; }

        public GetConfigurationQueryResponse_1_3(
            IEnumerable<GetConfigurationCategory_1_3> categories,
            IEnumerable<GetConfigurationNewsPortal_1_3> newsPortals
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

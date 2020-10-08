using System.Collections.Generic;

namespace Espresso.WebApi.Application.Configuration.Queries.GetWebConfiguration
{
    public class GetWebConfigurationQueryResponse
    {
        public IEnumerable<GetWebConfigurationCategory> Categories { get; init; } = new List<GetWebConfigurationCategory>();
        public IEnumerable<int> NewsPortalIds { get; init; } = new List<int>();
    }
}

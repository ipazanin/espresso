// GetWebConfigurationQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Configuration.Queries.GetWebConfiguration;

public class GetWebConfigurationQueryResponse
{
    public IEnumerable<GetWebConfigurationCategory> Categories { get; init; } = new List<GetWebConfigurationCategory>();

    public IEnumerable<int> NewsPortalIds { get; init; } = new List<int>();
}

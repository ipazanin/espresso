// GetConfigurationQueryResponse_1_3.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Collections.Generic;

namespace Espresso.WebApi.Application.Configuration.Queries.GetConfiguration_1_3
{
    public record GetConfigurationQueryResponse_1_3
    {
        public IEnumerable<GetConfigurationCategory_1_3> Categories { get; init; } = new List<GetConfigurationCategory_1_3>();

        public IEnumerable<GetConfigurationNewsPortal_1_3> NewsPortals { get; init; } = new List<GetConfigurationNewsPortal_1_3>();
    }
}

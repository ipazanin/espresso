// GetConfigurationQueryResponse_1_3.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Configuration.Queries.GetConfiguration_1_3;
#pragma warning disable S101 // Types should be named in PascalCase
public record GetConfigurationQueryResponse_1_3
#pragma warning restore S101 // Types should be named in PascalCase
{
    public IEnumerable<GetConfigurationCategory_1_3> Categories { get; init; } = new List<GetConfigurationCategory_1_3>();

    public IEnumerable<GetConfigurationNewsPortal_1_3> NewsPortals { get; init; } = new List<GetConfigurationNewsPortal_1_3>();
}

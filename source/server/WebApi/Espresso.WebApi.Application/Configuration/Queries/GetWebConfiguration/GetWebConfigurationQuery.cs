// GetWebConfigurationQuery.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application.Configuration.Queries.GetWebConfiguration;

public record GetWebConfigurationQuery : Request<GetWebConfigurationQueryResponse>
{
}

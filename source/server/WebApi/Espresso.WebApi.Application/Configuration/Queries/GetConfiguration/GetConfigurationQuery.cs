// GetConfigurationQuery.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application.Configuration.Queries.GetConfiguration
{
    public record GetConfigurationQuery : Request<GetConfigurationQueryResponse>
    {
    }
}

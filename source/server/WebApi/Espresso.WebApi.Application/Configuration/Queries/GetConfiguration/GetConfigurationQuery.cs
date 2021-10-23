// GetConfigurationQuery.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;
using System;

namespace Espresso.WebApi.Application.Configuration.Queries.GetConfiguration
{
    public record GetConfigurationQuery : Request<GetConfigurationQueryResponse>
    {
    }
}

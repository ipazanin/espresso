// GetWebConfigurationQuery.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application.Configuration.Queries.GetWebConfiguration
{
    public record GetWebConfigurationQuery : Request<GetWebConfigurationQueryResponse>
    {
        public TimeSpan MaxAgeOfNewNewsPortal { get; init; }
    }
}

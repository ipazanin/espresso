using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application.Configuration.Queries.GetConfiguration
{
    public record GetConfigurationQuery : Request<GetConfigurationQueryResponse>
    {
        public TimeSpan MaxAgeOfNewNewsPortal { get; init; }
    }
}

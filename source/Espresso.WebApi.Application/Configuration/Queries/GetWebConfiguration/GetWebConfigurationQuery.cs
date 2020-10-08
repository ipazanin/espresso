using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application.Configuration.Queries.GetWebConfiguration
{
    public record GetWebConfigurationQuery : Request<GetWebConfigurationQueryResponse>
    {
        public TimeSpan MaxAgeOfNewNewsPortal { get; init; }
    }
}

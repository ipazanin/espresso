using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.WebApi.Application.Configuration.Queries.GetConfiguration
{
    public record GetConfigurationQuery : Request<GetConfigurationQueryResponse>
    {
        public TimeSpan MaxAgeOfNewNewsPortal { get; init; }
    }
}

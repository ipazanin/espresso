using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.WebApi.Application.Configuration.Queries.GetWebConfiguration
{
    public record GetWebConfigurationQuery : Request<GetWebConfigurationQueryResponse>
    {
        public TimeSpan MaxAgeOfNewNewsPortal { get; init; }
    }
}

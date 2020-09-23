using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.WebApi.Application.Configuration.Queries.GetConfiguration
{
    public class GetConfigurationQuery : Request<GetConfigurationQueryResponse>
    {
        public TimeSpan MaxAgeOfNewNewsPortal { get; }

        public GetConfigurationQuery(
            TimeSpan maxAgeOfNewNewsPortal,
            string currentEspressoWebApiVersion,
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            AppEnvironment appEnvironment
        ) : base(
            currentEspressoWebApiVersion: currentEspressoWebApiVersion,
            targetedEspressoWebApiVersion: targetedEspressoWebApiVersion,
            consumerVersion: consumerVersion,
            deviceType: deviceType,
            appEnvironment: appEnvironment,
            Event.GetConfigurationQuery
        )
        {
            MaxAgeOfNewNewsPortal = maxAgeOfNewNewsPortal;
        }

        public override string ToString()
        {
            return $"{nameof(MaxAgeOfNewNewsPortal)}:{MaxAgeOfNewNewsPortal}";
        }
    }
}

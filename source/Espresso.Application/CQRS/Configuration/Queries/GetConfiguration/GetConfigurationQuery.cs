using Espresso.Application.Infrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.Application.CQRS.Configuration.Queries.GetConfiguration
{
    public class GetConfigurationQuery : Request<GetConfigurationQueryResponse>
    {
        public GetConfigurationQuery(
            string currentEspressoWebApiVersion,
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType
        ) : base(
            currentEspressoWebApiVersion: currentEspressoWebApiVersion,
            targetedEspressoWebApiVersion: targetedEspressoWebApiVersion,
          consumerVersion: consumerVersion,
            deviceType: deviceType,
            Event.GetConfigurationQuery
        )
        {
        }

        public override string ToString()
        {
            return "";
        }
    }
}

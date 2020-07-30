using Espresso.Application.Infrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.Application.CQRS.Configuration.Queries.GetConfiguration_1_3
{
    public class GetConfigurationQuery_1_3 : Request<GetConfigurationQueryResponse_1_3>
    {
        public GetConfigurationQuery_1_3(
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

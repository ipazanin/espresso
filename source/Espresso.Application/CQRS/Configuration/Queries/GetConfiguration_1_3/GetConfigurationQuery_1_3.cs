using Espresso.Application.Infrastructure;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Common.Enums;

namespace Espresso.Application.CQRS.Configuration.Queries.GetConfiguration_1_3
{
    public class GetConfigurationQuery_1_3 : Request<GetConfigurationQueryResponse_1_3>
    {
        public GetConfigurationQuery_1_3(
            string currentEspressoWebApiVersion,
            string espressoWebApiVersion,
            string version,
            DeviceType deviceType
        ) : base(
            currentEspressoWebApiVersion,
            espressoWebApiVersion,
            version,
            deviceType,
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

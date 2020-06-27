using Espresso.Application.Infrastructure;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Common.Enums;

namespace Espresso.Application.CQRS.Configuration.Query.GetConfiguration
{
    public class GetConfigurationQuery : Request<GetConfigurationQueryResponse>
    {
        public GetConfigurationQuery(
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

using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals_1_3
{
    public class GetNewsPortalsQuery_1_3 : Request<GetNewsPortalsQueryResponse_1_3>
    {
        public GetNewsPortalsQuery_1_3(
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
            Event.GetNewsPortalsQuery
        )
        {
        }

        public override string ToString()
        {
            return "";
        }
    }
}

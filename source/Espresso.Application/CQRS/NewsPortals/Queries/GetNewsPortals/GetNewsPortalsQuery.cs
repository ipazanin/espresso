using Espresso.Application.Infrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.Application.CQRS.NewsPortals.Queries.GetNewsPortals
{
    public class GetNewsPortalsQuery : Request<GetNewsPortalsQueryResponse>
    {
        public GetNewsPortalsQuery(
            string currentEspressoWebApiVersion,
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType
        ) : base(
            currentEspressoWebApiVersion: currentEspressoWebApiVersion,
            targetedEspressoWebApiVersion: targetedEspressoWebApiVersion,
          consumerVersion: consumerVersion,
            deviceType: deviceType,
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

using Espresso.Application.Infrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.Application.CQRS.NewsPortals.Queries.GetNewsPortals
{
    public class GetNewsPortalsQuery : Request<GetNewsPortalsQueryResponse>
    {
        public GetNewsPortalsQuery(
            string currentEspressoWebApiVersion,
            string espressoWebApiVersion,
            string version,
            DeviceType deviceType
        ) : base(
            currentEspressoWebApiVersion,
            espressoWebApiVersion,
            version,
            deviceType,
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

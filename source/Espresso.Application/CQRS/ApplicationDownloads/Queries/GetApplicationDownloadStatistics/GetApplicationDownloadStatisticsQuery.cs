using Espresso.Application.Infrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.Application.CQRS.ApplicationDownloads.Queries.GetApplicationDownloadStatistics
{
    public class GetApplicationDownloadStatisticsQuery : Request<GetApplicationDownloadStatisticsQueryResponse>
    {
        public GetApplicationDownloadStatisticsQuery(
            string currentEspressoWebApiVersion,
            string espressoWebApiVersion,
            string version,
            DeviceType deviceType
        ) : base(
            currentEspressoWebApiVersion,
            espressoWebApiVersion,
            version,
            deviceType,
            Event.GetApplicationDownloadStatisticsQuery
        )
        {
        }

        public override string ToString()
        {
            return "";
        }
    }
}

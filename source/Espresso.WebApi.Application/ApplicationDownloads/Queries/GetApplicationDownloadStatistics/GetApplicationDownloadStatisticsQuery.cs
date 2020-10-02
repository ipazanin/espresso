using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.WebApi.Application.ApplicationDownloads.Queries.GetApplicationDownloadStatistics
{
    public record GetApplicationDownloadStatisticsQuery : Request<GetApplicationDownloadStatisticsQueryResponse>
    {
    }
}

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Enums;
using Espresso.Persistence.IRepositories;
using MediatR;

namespace Espresso.WebApi.Application.ApplicationDownloads.Queries.GetApplicationDownloadStatistics
{
    public class GetApplicationDownloadStatisticsQueryHandler : IRequestHandler<GetApplicationDownloadStatisticsQuery, GetApplicationDownloadStatisticsQueryResponse>
    {
        #region Fields
        private readonly IApplicationDownloadRepository _applicationDownloadRepository;
        #endregion

        #region Constructors
        public GetApplicationDownloadStatisticsQueryHandler(
            IApplicationDownloadRepository applicationDownloadRepository
        )
        {
            _applicationDownloadRepository = applicationDownloadRepository;
        }
        #endregion

        #region Methods
        public async Task<GetApplicationDownloadStatisticsQueryResponse> Handle(GetApplicationDownloadStatisticsQuery request, CancellationToken cancellationToken)
        {
            var applicationDownloads = await _applicationDownloadRepository.GetApplicationDownloads();

            var androidCount = applicationDownloads.Count(
                predicate: applicationDownload => applicationDownload.MobileDeviceType == DeviceType.Android
            );

            var iosCount = applicationDownloads.Count(
                predicate: applicationDownload => applicationDownload.MobileDeviceType == DeviceType.Ios
            );
            var response = new GetApplicationDownloadStatisticsQueryResponse
            {
                AndroidDownloadsCount = androidCount,
                IosDownloadsCount = iosCount,
                TotalDownloadCount = androidCount + iosCount
            };

            return response;
        }
        #endregion
    }
}

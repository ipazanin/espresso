using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

using MediatR;

using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.CQRS.ApplicationDownloads.Queries.GetApplicationDownloadStatistics
{
    public class GetApplicationDownloadStatisticsQueryHandler : IRequestHandler<GetApplicationDownloadStatisticsQuery, GetApplicationDownloadStatisticsQueryResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Constructors
        public GetApplicationDownloadStatisticsQueryHandler(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<GetApplicationDownloadStatisticsQueryResponse> Handle(GetApplicationDownloadStatisticsQuery request, CancellationToken cancellationToken)
        {
            var applicationDownloads = _memoryCache
                .Get<IEnumerable<ApplicationDownload>>(
                    key: MemoryCacheConstants.ApplicationDownloadKey
                );

            var androidCount = applicationDownloads.Count(
                predicate: applicationDownload => applicationDownload.MobileDeviceType == DeviceType.Android
            );

            var iosCount = applicationDownloads.Count(
                predicate: applicationDownload => applicationDownload.MobileDeviceType == DeviceType.Ios
            );
            var response = new GetApplicationDownloadStatisticsQueryResponse(androidCount, iosCount, androidCount + iosCount);

            return Task.FromResult(result: response);
        }
        #endregion
    }
}

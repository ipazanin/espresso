// GetApplicationDownloadStatisticsQueryHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Enums;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.WebApi.Application.ApplicationDownloads.Queries.GetApplicationDownloadStatistics;

public class GetApplicationDownloadStatisticsQueryHandler : IRequestHandler<GetApplicationDownloadStatisticsQuery, GetApplicationDownloadStatisticsQueryResponse>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetApplicationDownloadStatisticsQueryHandler"/> class.
    /// </summary>
    /// <param name="espressoDatabaseContext"></param>
    public GetApplicationDownloadStatisticsQueryHandler(
        IEspressoDatabaseContext espressoDatabaseContext)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
    }

    public async Task<GetApplicationDownloadStatisticsQueryResponse> Handle(GetApplicationDownloadStatisticsQuery request, CancellationToken cancellationToken)
    {
        var applicationDownloads = await _espressoDatabaseContext
            .ApplicationDownload
            .ToListAsync(cancellationToken);

        var androidCount = applicationDownloads.Count(
            predicate: applicationDownload => applicationDownload.MobileDeviceType == DeviceType.Android);

        var iosCount = applicationDownloads.Count(
            predicate: applicationDownload => applicationDownload.MobileDeviceType == DeviceType.Ios);
        var response = new GetApplicationDownloadStatisticsQueryResponse
        {
            AndroidDownloadsCount = androidCount,
            IosDownloadsCount = iosCount,
            TotalDownloadCount = androidCount + iosCount,
        };

        return response;
    }
}

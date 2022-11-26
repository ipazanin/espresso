// GetApplicationDownloadStatisticsQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.ApplicationDownloads.Queries.GetApplicationDownloadStatistics;

public record GetApplicationDownloadStatisticsQueryResponse
{
    public int AndroidDownloadsCount { get; init; }

    public int IosDownloadsCount { get; init; }

    public int TotalDownloadCount { get; init; }
}

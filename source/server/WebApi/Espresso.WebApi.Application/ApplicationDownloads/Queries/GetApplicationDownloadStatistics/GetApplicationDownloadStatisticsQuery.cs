// GetApplicationDownloadStatisticsQuery.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Enums;
using MediatR;

namespace Espresso.WebApi.Application.ApplicationDownloads.Queries.GetApplicationDownloadStatistics;

public record GetApplicationDownloadStatisticsQuery : IRequest<GetApplicationDownloadStatisticsQueryResponse>
{
    /// <summary>
    /// Gets targeted api version.
    /// </summary>
    public string TargetedApiVersion { get; init; } = string.Empty;

    /// <summary>
    /// Gets consumer version.
    /// </summary>
    public string ConsumerVersion { get; init; } = string.Empty;

    /// <summary>
    /// Gets device type.
    /// </summary>
    public DeviceType DeviceType { get; init; }
}

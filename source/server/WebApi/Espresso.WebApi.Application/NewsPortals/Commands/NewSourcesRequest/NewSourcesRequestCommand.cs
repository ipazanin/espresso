// NewSourcesRequestCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Enums;
using MediatR;

namespace Espresso.WebApi.Application.NewsPortals.Commands.NewSourcesRequest;

public record NewsSourcesRequestCommand : IRequest
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

    public string NewsPortalName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string? Url { get; init; }
}

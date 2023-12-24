// SendPushNotificationCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Enums;
using MediatR;

namespace Espresso.WebApi.Application.Notifications.Commands.SendPushNotification;

public record SendPushNotificationCommand : IRequest
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

    public Guid ArticleId { get; init; }
    public string InternalName { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public string Topic { get; init; } = string.Empty;
    public string ArticleUrl { get; init; } = string.Empty;
    public bool IsSoundEnabled { get; init; }
}

// SendArticlesNotificationsCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Enums;
using MediatR;

namespace Espresso.WebApi.Application.Notifications.Commands.SendArticlesNotifications;

public record SendArticlesNotificationsCommand : IRequest
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

    public IEnumerable<Guid> CreatedArticleIds { get; init; } = new List<Guid>();

    public IEnumerable<Guid> UpdatedArticleIds { get; init; } = new List<Guid>();
}

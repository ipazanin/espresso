// SendArticlesNotificationsCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;
using MediatR;

namespace Espresso.WebApi.Application.Notifications.Commands.SendArticlesNotifications;

public record SendArticlesNotificationsCommand : Request<Unit>
{
    public IEnumerable<Guid> CreatedArticleIds { get; init; } = new List<Guid>();

    public IEnumerable<Guid> UpdatedArticleIds { get; init; } = new List<Guid>();
}

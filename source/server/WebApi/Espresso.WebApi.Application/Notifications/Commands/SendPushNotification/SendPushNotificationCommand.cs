// SendPushNotificationCommand.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;
using MediatR;
using System;

namespace Espresso.WebApi.Application.Notifications.Commands.SendPushNotification
{
    public record SendPushNotificationCommand : Request<Unit>
    {
        public Guid ArticleId { get; init; }
        public string InternalName { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
        public string Topic { get; init; } = string.Empty;
        public string ArticleUrl { get; init; } = string.Empty;
        public bool IsSoundEnabled { get; init; }
    }
}

using Espresso.Application.Infrastructure.MediatorInfrastructure;
using MediatR;

namespace Espresso.WebApi.Application.Notifications.Commands.SendPushNotification
{
    public record SendPushNotificationCommand : Request<Unit>
    {
        public string InternalName { get; init; } = "";
        public string Title { get; init; } = "";
        public string Message { get; init; } = "";
        public string Topic { get; init; } = "";
        public string ArticleUrl { get; init; } = "";
        public bool IsSoundEnabled { get; init; }
    }
}

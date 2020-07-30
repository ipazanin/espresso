using Espresso.Application.Infrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using MediatR;

namespace Espresso.Application.CQRS.Notifications.Commands.SendPushNotification
{
    public class SendPushNotificationCommand : Request<Unit>
    {
        public string InternalName { get; }
        public string Title { get; }
        public string Message { get; }
        public string Topic { get; }
        public string ArticleUrl { get; }
        public bool IsSoundEnabled { get; }

        public SendPushNotificationCommand(
            string? internalName,
            string? title,
            string? message,
            string? topic,
            string? articleUrl,
            bool isSoundEnabled,
            string currentEspressoWebApiVersion,
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType
        ) : base(
            currentEspressoWebApiVersion: currentEspressoWebApiVersion,
            targetedEspressoWebApiVersion: targetedEspressoWebApiVersion,
          consumerVersion: consumerVersion,
            deviceType: deviceType,
            Event.SendPushNotification
        )
        {
            InternalName = internalName ?? "";
            Title = title ?? "";
            Message = message ?? "";
            Topic = topic ?? "";
            ArticleUrl = articleUrl ?? "";
            IsSoundEnabled = isSoundEnabled;
        }

        public override string ToString()
        {
            return
            $"{nameof(InternalName)}:{InternalName}, " +
            $"{nameof(Title)}:{Title}, " +
            $"{nameof(Message)}:{Message}, " +
            $"{nameof(Topic)}:{Topic}, " +
            $"{nameof(ArticleUrl)}:{ArticleUrl}, " +
            $"{nameof(IsSoundEnabled)}:{IsSoundEnabled}";
        }
    }

}

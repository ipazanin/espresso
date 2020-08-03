
using System.Collections.Generic;

namespace Espresso.Application.CQRS.Notifications.Queries.GetPushNotifications
{
    public class GetPushNotificationsQueryResponse
    {
        public IEnumerable<GetPushNotificationsPushNotification> PushNotifications { get; }

        public GetPushNotificationsQueryResponse(
            IEnumerable<GetPushNotificationsPushNotification> pushNotifications
        )
        {
            PushNotifications = pushNotifications;
        }
    }
}

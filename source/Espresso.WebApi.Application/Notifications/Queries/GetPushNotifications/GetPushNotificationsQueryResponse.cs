
using System.Collections.Generic;

namespace Espresso.WebApi.Application.Notifications.Queries.GetPushNotifications
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


using System.Collections.Generic;

namespace Espresso.Application.CQRS.Notifications.Queries.GetPushNotifications
{
    public class GetPushNotificationsQueryResponse
    {
        public IEnumerable<PushNotificationDto> PushNotifications { get; }

        public GetPushNotificationsQueryResponse(
            IEnumerable<PushNotificationDto> pushNotifications
        )
        {
            PushNotifications = pushNotifications;
        }
    }
}

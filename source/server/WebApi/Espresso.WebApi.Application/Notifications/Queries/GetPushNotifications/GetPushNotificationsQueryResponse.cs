// GetPushNotificationsQueryResponse.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Collections.Generic;

namespace Espresso.WebApi.Application.Notifications.Queries.GetPushNotifications
{
    public class GetPushNotificationsQueryResponse
    {
        public IEnumerable<GetPushNotificationsPushNotification> PushNotifications { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPushNotificationsQueryResponse"/> class.
        /// </summary>
        /// <param name="pushNotifications"></param>
        public GetPushNotificationsQueryResponse(
            IEnumerable<GetPushNotificationsPushNotification> pushNotifications
        )
        {
            PushNotifications = pushNotifications;
        }
    }
}

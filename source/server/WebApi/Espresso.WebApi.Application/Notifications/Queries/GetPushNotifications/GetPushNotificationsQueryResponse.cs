// GetPushNotificationsQueryResponse.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Notifications.Queries.GetPushNotifications
{
    public class GetPushNotificationsQueryResponse
    {
        public IEnumerable<GetPushNotificationsPushNotification> PushNotifications { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPushNotificationsQueryResponse"/> class.
        /// </summary>
        /// <param name="pushNotifications"></param>
#pragma warning disable SA1201 // Elements should appear in the correct order
        public GetPushNotificationsQueryResponse(
#pragma warning restore SA1201 // Elements should appear in the correct order
            IEnumerable<GetPushNotificationsPushNotification> pushNotifications)
        {
            PushNotifications = pushNotifications;
        }
    }
}

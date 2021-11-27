// GetPushNotificationsQueryResponse.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Notifications.Queries.GetPushNotifications;

public class GetPushNotificationsQueryResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetPushNotificationsQueryResponse"/> class.
    /// </summary>
    /// <param name="pushNotifications"></param>
    public GetPushNotificationsQueryResponse(
        IEnumerable<GetPushNotificationsPushNotification> pushNotifications)
    {
        PushNotifications = pushNotifications;
    }

    public IEnumerable<GetPushNotificationsPushNotification> PushNotifications { get; }
}

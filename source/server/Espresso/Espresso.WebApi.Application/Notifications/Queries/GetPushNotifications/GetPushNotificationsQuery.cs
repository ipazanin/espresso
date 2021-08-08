// GetPushNotificationsQuery.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application.Notifications.Queries.GetPushNotifications
{
    public record GetPushNotificationsQuery : Request<GetPushNotificationsQueryResponse>
    {
        public int Take { get; init; }
        public int Skip { get; init; }
    }
}

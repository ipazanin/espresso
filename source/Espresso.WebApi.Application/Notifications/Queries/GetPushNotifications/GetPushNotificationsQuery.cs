
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.WebApi.Application.Notifications.Queries.GetPushNotifications
{
    public record GetPushNotificationsQuery : Request<GetPushNotificationsQueryResponse>
    {
        public int Take { get; init; }
        public int Skip { get; init; }
    }
}

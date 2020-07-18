
using Espresso.Application.Infrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.Application.CQRS.Notifications.Queries.GetPushNotifications
{
    public class GetPushNotificationsQuery : Request<GetPushNotificationsQueryResponse>
    {
        public int Take { get; }
        public int Skip { get; }

        public GetPushNotificationsQuery(
            int take,
            int skip,
            string currentEspressoWebApiVersion,
            string espressoWebApiVersion,
            string version,
            DeviceType deviceType
        ) : base(
            currentEspressoWebApiVersion,
            espressoWebApiVersion,
            version,
            deviceType,
            Event.GetPushNotifications
        )
        {
            Take = take;
            Skip = skip;
        }
    }
}

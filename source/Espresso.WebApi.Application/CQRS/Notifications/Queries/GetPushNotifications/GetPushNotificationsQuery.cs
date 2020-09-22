
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.WebApi.Application.CQRS.Notifications.Queries.GetPushNotifications
{
    public class GetPushNotificationsQuery : Request<GetPushNotificationsQueryResponse>
    {
        public int Take { get; }
        public int Skip { get; }

        public GetPushNotificationsQuery(
            int take,
            int skip,
            string currentEspressoWebApiVersion,
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            AppEnvironment appEnvironment
        ) : base(
            currentEspressoWebApiVersion: currentEspressoWebApiVersion,
            targetedEspressoWebApiVersion: targetedEspressoWebApiVersion,
            consumerVersion: consumerVersion,
            deviceType: deviceType,
            appEnvironment: appEnvironment,
            Event.GetPushNotifications
        )
        {
            Take = take;
            Skip = skip;
        }
    }
}

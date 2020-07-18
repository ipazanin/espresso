using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Application.CQRS.Notifications.Queries.GetPushNotifications
{
    public class GetPushNotificationsQueryHandler : IRequestHandler<GetPushNotificationsQuery, GetPushNotificationsQueryResponse>
    {
        private readonly IEspressoDatabaseContext _espressoDatabaseContext;

        public GetPushNotificationsQueryHandler(
            IEspressoDatabaseContext espressoDatabaseContext
        )
        {
            _espressoDatabaseContext = espressoDatabaseContext;
        }
        public async Task<GetPushNotificationsQueryResponse> Handle(
            GetPushNotificationsQuery request,
            CancellationToken cancellationToken
        )
        {
            var pushNotifications = await _espressoDatabaseContext
                .PushNotifications
                .OrderBy(keySelector: PushNotification.GetOrderByDescendingExpression())
                .Select(selector: PushNotificationDto.GetProjection())
                .ToListAsync(cancellationToken: cancellationToken);

            return new GetPushNotificationsQueryResponse(pushNotifications: pushNotifications);
        }
    }
}

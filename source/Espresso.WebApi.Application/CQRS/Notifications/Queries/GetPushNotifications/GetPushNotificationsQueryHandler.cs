using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.WebApi.Application.CQRS.Notifications.Queries.GetPushNotifications
{
    public class GetPushNotificationsQueryHandler : IRequestHandler<GetPushNotificationsQuery, GetPushNotificationsQueryResponse>
    {
        private readonly IApplicationDatabaseContext _espressoDatabaseContext;

        public GetPushNotificationsQueryHandler(
            IApplicationDatabaseContext espressoDatabaseContext
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
                .OrderByDescending(keySelector: PushNotification.GetOrderByDescendingExpression())
                .Skip(count: request.Skip)
                .Take(count: request.Take)
                .Select(selector: GetPushNotificationsPushNotification.GetProjection())
                .ToListAsync(cancellationToken: cancellationToken);

            return new GetPushNotificationsQueryResponse(pushNotifications: pushNotifications);
        }
    }
}

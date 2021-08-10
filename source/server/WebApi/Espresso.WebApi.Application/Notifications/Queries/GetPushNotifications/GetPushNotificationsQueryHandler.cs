// GetPushNotificationsQueryHandler.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.WebApi.Application.Notifications.Queries.GetPushNotifications
{
    public class GetPushNotificationsQueryHandler : IRequestHandler<GetPushNotificationsQuery, GetPushNotificationsQueryResponse>
    {
        private readonly IEspressoDatabaseContext _espressoDatabaseContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPushNotificationsQueryHandler"/> class.
        /// </summary>
        /// <param name="espressoDatabaseContext"></param>
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
                .OrderByDescending(keySelector: PushNotification.GetOrderByDescendingExpression())
                .Skip(count: request.Skip)
                .Take(count: request.Take)
                .Select(selector: GetPushNotificationsPushNotification.GetProjection())
                .ToListAsync(cancellationToken: cancellationToken);

            return new GetPushNotificationsQueryResponse(pushNotifications: pushNotifications);
        }
    }
}

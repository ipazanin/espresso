using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using FirebaseAdmin.Messaging;
using MediatR;

namespace Espresso.Application.CQRS.Notifications.Commands.SendPushNotification
{
    public class SendPushNotificationCommandHandler : IRequestHandler<SendPushNotificationCommand>
    {
        #region Constants
        private const string DefaultSoundName = "default";
        private const string DisabledSoundName = "";
        private const int DefaultBadge = 1;
        #endregion

        #region Fields
        private readonly IApplicationDatabaseContext _espressoDatabaseContext;
        #endregion

        #region Constructors
        public SendPushNotificationCommandHandler(
            IApplicationDatabaseContext espressoDatabaseContext
        )
        {
            _espressoDatabaseContext = espressoDatabaseContext;
        }
        #endregion

        #region Methods
        public async Task<Unit> Handle(SendPushNotificationCommand request, CancellationToken cancellationToken)
        {
            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = request.Title,
                    Body = request.Message,
                },
                Topic = request.Topic,
                Apns = new ApnsConfig
                {
                    Aps = new Aps
                    {
                        Badge = DefaultBadge,
                        Sound = request.IsSoundEnabled ?
                            DefaultSoundName :
                            DisabledSoundName
                    }
                },
                Android = new AndroidConfig
                {
                    Data = new Dictionary<string, string>
                    {
                        { "title", request.Title },
                        { "message", request.Message },
                        { "url", request.ArticleUrl },
                    },

                },
                Data = new Dictionary<string, string>
                {
                    { "title", request.Title },
                    { "message", request.Message },
                    { "url", request.ArticleUrl },
                }
            };

            var messaging = FirebaseMessaging.DefaultInstance;

            _ = await messaging.SendAsync(message);
            var pushNotification = new PushNotification(
                id: Guid.NewGuid(),
                internalName: request.InternalName,
                title: request.Title,
                message: request.Message,
                topic: request.Topic,
                articleUrl: request.ArticleUrl,
                isSoundEnabled: request.IsSoundEnabled,
                createdAt: DateTime.UtcNow
            );

            _ = _espressoDatabaseContext.PushNotifications.Add(pushNotification);
            _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken: cancellationToken);

            return Unit.Value;
        }
        #endregion
    }
}

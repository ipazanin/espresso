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
        private const string DefaultSoundName = "default";
        private readonly IEspressoDatabaseContext _espressoDatabaseContext;

        public SendPushNotificationCommandHandler(
            IEspressoDatabaseContext espressoDatabaseContext
        )
        {
            _espressoDatabaseContext = espressoDatabaseContext;
        }

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
                        Badge = 1,
                        Sound = request.IsSoundEnabled ? DefaultSoundName : ""
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

            await messaging.SendAsync(message);
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

            _espressoDatabaseContext.PushNotifications.Add(pushNotification);
            await _espressoDatabaseContext.SaveChangesAsync(cancellationToken: cancellationToken);

            return Unit.Value;
        }
    }
}

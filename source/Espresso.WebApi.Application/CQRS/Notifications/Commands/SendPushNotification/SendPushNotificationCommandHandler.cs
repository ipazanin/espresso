using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using FirebaseAdmin.Messaging;
using MediatR;

namespace Espresso.WebApi.Application.CQRS.Notifications.Commands.SendPushNotification
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
            var internalName = GetInternalName(request.InternalName);
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
                        { "internalName", internalName },
                        { "title", request.Title },
                        { "message", request.Message },
                        { "url", request.ArticleUrl },
                    },

                },
                Data = new Dictionary<string, string>
                {
                    { "internalName", internalName },
                    { "title", request.Title },
                    { "message", request.Message },
                    { "url", request.ArticleUrl },
                }
            };

            var messaging = FirebaseMessaging.DefaultInstance;

            _ = await messaging.SendAsync(message, cancellationToken);
            var pushNotification = new PushNotification(
                id: Guid.NewGuid(),
                internalName: internalName,
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

        private static string GetInternalName(string? internalName)
        {
            return string.IsNullOrWhiteSpace(internalName) ?
                $"ESPR-{DateTime.UtcNow.ToString(DateTimeConstants.PushNotificationInternalNameFormat)}" :
                internalName;
        }
        #endregion
    }
}

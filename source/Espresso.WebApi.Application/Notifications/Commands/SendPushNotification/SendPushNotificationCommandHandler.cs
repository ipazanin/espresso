using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Common.Utilities;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using Espresso.WebApi.Application.Exceptions;
using FirebaseAdmin.Messaging;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Notifications.Commands.SendPushNotification
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
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Constructors
        public SendPushNotificationCommandHandler(
            IApplicationDatabaseContext espressoDatabaseContext,
            IMemoryCache memoryCache
        )
        {
            _espressoDatabaseContext = espressoDatabaseContext;
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public async Task<Unit> Handle(SendPushNotificationCommand request, CancellationToken cancellationToken)
        {
            if (request.AppEnvironment != Common.Enums.AppEnvironment.Prod)
            {
                return Unit.Value;
            }

            var articles = _memoryCache.Get<IEnumerable<Article>>(MemoryCacheConstants.ArticleKey);
            var pushNotificationArticle = articles.FirstOrDefault(article => article.Id == request.ArticleId);

            if (pushNotificationArticle is null)
            {
                throw new NotFoundException("Article was not found!");
            }

            var articleDto = SendPushNotificationArticle
                .GetProjection()
                .Compile()
                .Invoke(pushNotificationArticle);

            var articleDtoJsonString = await JsonUtility.Serialize(articleDto, cancellationToken);

            var internalName = GetInternalName(request.InternalName);
            var customData = new Dictionary<string, string>
            {
                { "internalName", internalName },
                { "title", request.Title },
                { "message", request.Message },
                { "url", request.ArticleUrl },
                { "article", articleDtoJsonString },
            };

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
                Data = customData
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

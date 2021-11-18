// GetPushNotificationsPushNotification.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using System.Linq.Expressions;

namespace Espresso.WebApi.Application.Notifications.Queries.GetPushNotifications
{
    public class GetPushNotificationsPushNotification
    {
        public string InternalName { get; private set; }

        public string Title { get; private set; }

        public string Message { get; private set; }

        public string Topic { get; private set; }

        public string ArticleUrl { get; private set; }

        public bool IsSoundEnabled { get; private set; }

        public string CreatedAt { get; private set; }

#pragma warning disable SA1201 // Elements should appear in the correct order
        private GetPushNotificationsPushNotification()
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
            InternalName = null!;
            Title = null!;
            Message = null!;
            Topic = null!;
            ArticleUrl = null!;
            CreatedAt = null!;
        }

        public static Expression<Func<PushNotification, GetPushNotificationsPushNotification>> GetProjection()
        {
            return pushNotification => new GetPushNotificationsPushNotification
            {
                InternalName = pushNotification.InternalName,
                Title = pushNotification.Title,
                Message = pushNotification.Message,
                Topic = pushNotification.Topic,
                ArticleUrl = pushNotification.ArticleUrl,
                IsSoundEnabled = pushNotification.IsSoundEnabled,
                CreatedAt = pushNotification.CreatedAt.ToString(DateTimeConstants.MobileAppDateTimeFormat),
            };
        }
    }
}

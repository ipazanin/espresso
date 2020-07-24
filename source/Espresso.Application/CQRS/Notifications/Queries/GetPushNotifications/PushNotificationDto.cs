
using System;
using System.Linq.Expressions;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;

namespace Espresso.Application.CQRS.Notifications.Queries.GetPushNotifications
{
    public class PushNotificationDto
    {
        public string InternalName { get; private set; }
        public string Title { get; private set; }
        public string Message { get; private set; }
        public string Topic { get; private set; }
        public string ArticleUrl { get; private set; }
        public bool IsSoundEnabled { get; private set; }
        public string CreatedAt { get; private set; }

        private PushNotificationDto()
        {
            InternalName = null!;
            Title = null!;
            Message = null!;
            Topic = null!;
            ArticleUrl = null!;
            CreatedAt = null!;
        }

        public static Expression<Func<PushNotification, PushNotificationDto>> GetProjection()
        {
            return pushNotification => new PushNotificationDto
            {
                InternalName = pushNotification.InternalName,
                Title = pushNotification.Title,
                Message = pushNotification.Message,
                Topic = pushNotification.Topic,
                ArticleUrl = pushNotification.ArticleUrl,
                IsSoundEnabled = pushNotification.IsSoundEnabled,
                CreatedAt = pushNotification.CreatedAt.ToString(DateTimeConstants.MobileAppDateTimeFormat)
            };
        }
    }
}

using System;
using System.Linq.Expressions;

namespace Espresso.Domain.Entities
{
    public class PushNotification
    {
        #region Constants
        public const int InternalNameMaxLength = 1000;
        public const int TitleMaxLength = 1000;
        public const int MessageMaxLength = 1000;
        public const int TopicMaxLength = 1000;
        public const int ArticleUrlMaxLength = 5000;
        #endregion

        #region Properties
        public Guid Id { get; private set; }
        public string InternalName { get; private set; }
        public string Title { get; private set; }
        public string Message { get; private set; }
        public string Topic { get; private set; }
        public string ArticleUrl { get; private set; }
        public bool IsSoundEnabled { get; private set; }
        public DateTime CreatedAt { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// ORM Materialisation Constructor
        /// </summary>
        private PushNotification()
        {
            InternalName = null!;
            Title = null!;
            Message = null!;
            Topic = null!;
            ArticleUrl = null!;
        }

        public PushNotification(
            Guid id,
            string internalName,
            string title,
            string message,
            string topic,
            string articleUrl,
            bool isSoundEnabled,
            DateTime createdAt
        )
        {
            Id = id;
            InternalName = internalName;
            Title = title;
            Message = message;
            Topic = topic;
            ArticleUrl = articleUrl;
            IsSoundEnabled = isSoundEnabled;
            CreatedAt = createdAt;
        }
        #endregion

        #region Methods
        public static Expression<Func<PushNotification, object>> GetOrderByDescendingExpression()
        {
            return pushNotification => pushNotification.CreatedAt;
        }
        #endregion
    }
}

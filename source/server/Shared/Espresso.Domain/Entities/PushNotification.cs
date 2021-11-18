﻿// PushNotification.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Linq.Expressions;

namespace Espresso.Domain.Entities
{
    public class PushNotification
    {
        public const int InternalNameMaxLength = 1000;
        public const int TitleMaxLength = 1000;
        public const int MessageMaxLength = 1000;
        public const int TopicMaxLength = 1000;
        public const int ArticleUrlMaxLength = 5000;

        public Guid Id { get; private set; }

        public string InternalName { get; private set; }

        public string Title { get; private set; }

        public string Message { get; private set; }

        public string Topic { get; private set; }

        public string ArticleUrl { get; private set; }

        public bool IsSoundEnabled { get; private set; }

        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PushNotification"/> class.
        /// ORM Materialisation Constructor.
        /// </summary>
#pragma warning disable SA1201 // Elements should appear in the correct order
        private PushNotification()
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
            InternalName = null!;
            Title = null!;
            Message = null!;
            Topic = null!;
            ArticleUrl = null!;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PushNotification"/> class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="internalName"></param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="topic"></param>
        /// <param name="articleUrl"></param>
        /// <param name="isSoundEnabled"></param>
        /// <param name="createdAt"></param>
        public PushNotification(
            Guid id,
            string internalName,
            string title,
            string message,
            string topic,
            string articleUrl,
            bool isSoundEnabled,
            DateTime createdAt)
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

        public static Expression<Func<PushNotification, object>> GetOrderByDescendingExpression()
        {
            return pushNotification => pushNotification.CreatedAt;
        }
    }
}

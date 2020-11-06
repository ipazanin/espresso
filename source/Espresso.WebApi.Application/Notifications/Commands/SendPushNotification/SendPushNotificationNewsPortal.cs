﻿using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Notifications.Commands.SendPushNotification
{
    public record SendPushNotificationNewsPortal
    {
        #region Properties
        /// <summary>
        /// News Portal ID
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// News Portal Name
        /// </summary>
        public string Name { get; private set; } = "";

        public string IconUrl { get; private set; } = "";
        #endregion

        #region Constructors
        private SendPushNotificationNewsPortal()
        {
        }
        #endregion

        #region Methods
        public static Expression<Func<NewsPortal, SendPushNotificationNewsPortal>> GetProjection()
        {
            return newsPortal => new SendPushNotificationNewsPortal
            {
                Id = newsPortal.Id,
                Name = newsPortal.Name,
                IconUrl = newsPortal.IconUrl
            };
        }
        #endregion
    }
}
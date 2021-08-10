﻿// NotificationHub.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Espresso.WebApi.Application.Hubs
{
    public class ArticlesNotificationHub : Hub
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public Task AddToGroup(string groupName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public Task RemoveFromGroup(string groupName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
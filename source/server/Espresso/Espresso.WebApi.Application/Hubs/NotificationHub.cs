using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Espresso.WebApi.Application.Hubs
{
    public class ArticlesNotificationHub : Hub
    {
        public ArticlesNotificationHub()
        {
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}

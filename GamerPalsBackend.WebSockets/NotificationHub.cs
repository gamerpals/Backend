using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace GamerPalsBackend.WebSockets
{
    public class NotificationHub : Hub
    {
        public NotificationHub()
        {

        }
        public async Task SendNotification(string message, params string[] userIds)
        {
            await Clients.Users(userIds).SendAsync("ReceiveNotification", message);
        }
    }
}

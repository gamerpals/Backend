using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.SignalR;

namespace GamerPalsBackend.WebSockets
{
    public class PalsUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User.Claims.Single(c => c.Type.Equals("UserID")).Value;
        }
    }
}

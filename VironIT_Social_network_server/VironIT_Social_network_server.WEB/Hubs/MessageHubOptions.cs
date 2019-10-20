using Microsoft.AspNetCore.SignalR;

using System;
using System.Security.Claims;


namespace VironIT_Social_network_server.WEB.SignalR
{
    public class MessageHubUserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}

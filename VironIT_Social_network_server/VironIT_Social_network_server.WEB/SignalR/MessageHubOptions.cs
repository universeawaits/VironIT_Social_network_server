using Microsoft.AspNetCore.SignalR;

using System;
using System.Security.Claims;


namespace VironIT_Social_network_server.WEB.SignalR
{
    public class Message
    {
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Text { get; set; }
        public string Status { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class MessageHubUserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}

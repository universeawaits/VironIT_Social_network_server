using System;

using Microsoft.AspNetCore.Identity;


namespace VironIT_Social_network_server.WEB.Identity
{
    public class User : IdentityUser
    {
        public DateTime Registered { get; set; }
        public DateTime? LastSeen { get; set; }
        public bool IsOnline { get; set; }
    }

}

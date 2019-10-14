using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace VironIT_Social_network_server.WEB.Identity
{
    public class User : IdentityUser
    {
        public DateTime Registered { get; set; }
        public DateTime? LastSeen { get; set; }
        public bool IsOnline { get; set; }
    }

}

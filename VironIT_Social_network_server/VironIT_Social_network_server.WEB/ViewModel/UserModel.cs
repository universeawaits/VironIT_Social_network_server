using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VironIT_Social_network_server.WEB.ViewModel
{
    public class UserProfileModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Registered { get; set; }
        public string Avatar { get; set; }
    }
}

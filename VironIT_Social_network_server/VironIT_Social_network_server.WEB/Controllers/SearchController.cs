using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VironIT_Social_network_server.BLL.Services.Interface;
using VironIT_Social_network_server.WEB.Identity;

namespace VironIT_Social_network_server.WEB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private UserManager<User> manager;
        private IContactService contactService;

        public SearchController(UserManager<User> manager, IContactService contactService)
        {
            this.manager = manager;
            this.contactService = contactService;
        }

        public async Task<IActionResult> SearchByFilter([FromQuery(Name = "emailOrPhone")] string emailOrPhone)
        {
            Response.ContentType = "application/json";
            foreach (User _user in manager.Users)
            {
                if (_user.Email.StartsWith(emailOrPhone) || _user.PhoneNumber.StartsWith(emailOrPhone))
                {
                    var user = new
                    {
                        user = _user,

                    };
                }
            }

            return null;
        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using VironIT_Social_network_server.WEB.Identity;
using VironIT_Social_network_server.WEB.ViewModel;


namespace VironIT_Social_network_server.WEB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserManager<User> manager;

        public UsersController(UserManager<User> manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel user)
        {
            string username = user.Username;
            string email = user.Email;
            string phone = user.Phone;
            string password = user.Password;

            User newUser = new User
            {
                UserName = username,
                Email = email,
                PhoneNumber = phone,
                Registered = DateTime.UtcNow,
                LastSeen = null,
                IsOnline = false
            };
            IdentityResult result = await manager.CreateAsync(newUser, password);

            if (result.Succeeded)
            {
                return Created("users", "registered successfully");
            }
            else
            {
                string error = result.Errors.First().Description;
                return BadRequest(error.ToLower().Substring(0, error.Length - 1));
            }
        }

        public async Task Update([FromBody] )
    }
}
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using VironIT_Social_network_server.WEB.ViewModel;


namespace VironIT_Social_network_server.WEB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserManager<IdentityUser> manager;

        public UsersController(UserManager<IdentityUser> manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("register")]
        public async Task Register([FromBody] UserRegisterModel user)
        {
            string username = user.Username;
            string email = user.Email;
            string phone = user.Phone;
            string password = user.Password;

            IdentityUser newUser = new IdentityUser
            {
                UserName = username,
                Email = email,
                PhoneNumber = phone
            };
            IdentityResult result = await manager.CreateAsync(newUser, password);

            Response.ContentType = "text/plain";
            await Response.WriteAsync(result.Succeeded ? "Registered successfully." : $"{result.Errors.First().Description}");
        }
    }
}
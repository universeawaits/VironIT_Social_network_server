using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.BLL.Services.Interface;
using VironIT_Social_network_server.DAL.Model;
using VironIT_Social_network_server.WEB.Identity;
using VironIT_Social_network_server.WEB.IdentityProvider;
using VironIT_Social_network_server.WEB.ViewModels;


namespace VironIT_Social_network_server.WEB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserManager<User> manager;
        private IEmailService emailService;
        private IImageService imageSrevice;

        public UsersController(UserManager<User> manager, IEmailService emailService, IImageService imageSrevice)
        {
            this.manager = manager;
            this.emailService = emailService;
            this.imageSrevice = imageSrevice;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel user)
        {
            if (user == null)
            {
                return BadRequest("user was empty");
            }

            if (manager.Users.FirstOrDefault(
                    _user => _user.PhoneNumber.Equals(user.Phone)
                ) != null)
            {
                return BadRequest($"phone {user.Phone} is already taken");
            }

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
                bool isEmailSent = await emailService.SendAsync(
                    newUser.Email, 
                    "registration", 
                    $"dear {newUser.UserName}, welcome to the skies"
                    );
                if (!isEmailSent)
                {
                    await manager.DeleteAsync(newUser);
                    return BadRequest("no internet connection (can't send confirmation email)");
                }

                await imageSrevice.AddAvatarAsync(new AvatarDTO
                {
                    Link = "",
                    UserEmail = newUser.Email,
                    SizeCategory = AvatarSizeCategory.Large
                });
                await imageSrevice.AddAvatarAsync(new AvatarDTO
                {
                    Link = "",
                    UserEmail = newUser.Email,
                    SizeCategory = AvatarSizeCategory.Medium
                });

                return Created("users", "registered successfully");
            }
            else
            {
                string error = result.Errors.First().Description;
                return BadRequest(error.ToLower().Substring(0, error.Length - 1));
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData([FromBody] UserEditModel user)
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                return Unauthorized();
            }

            User foundUser = await manager.FindByEmailAsync(email);

            foundUser.UserName = user.Name;
            await manager.UpdateAsync(foundUser);

            if (!user.Password.Trim().Equals(""))
            {
                string token = await manager.GeneratePasswordResetTokenAsync(foundUser);
                IdentityResult resetResult = await manager.ResetPasswordAsync(foundUser, token, user.Password);
                if (resetResult.Succeeded)
                {
                    await emailService.SendAsync(
                            foundUser.Email,
                            "password",
                            $"dear {foundUser.UserName}, you password has changed"
                            );
                }
            }            

            return Ok();
        }
    }
}
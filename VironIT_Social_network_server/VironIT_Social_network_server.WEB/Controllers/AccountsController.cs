using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using VironIT_Social_network_server.WEB.Identity.JWT;
using VironIT_Social_network_server.WEB.ViewModel;
using VironIT_Social_network_server.WEB.Identity;
using VironIT_Social_network_server.BLL.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace VironIT_Social_network_server.WEB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private UserManager<User> manager;
        private IImageService imageService;

        public AccountsController(UserManager<User> manager, IImageService imageService)
        {
            this.manager = manager;
            this.imageService = imageService;
        }

        [HttpPost]
        [Route("token")]
        public async Task Token([FromBody] UserLoginModel user)
        {
            string emailOrPhone = user.EmailOrPhone;
            string password = user.Password;

            ClaimsIdentity identity = await GetIdentityAsync(emailOrPhone, password);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("invalid username or password");
                return;
            }

            DateTime now = DateTime.UtcNow;
            JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer: JwtOptions.Issuer,
                    audience: JwtOptions.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(JwtOptions.Lifetime)),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtOptions.Secret)), 
                        SecurityAlgorithms.HmacSha256)
                    );
            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                token = encodedJwt,
                email = identity.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email).Value
            };

            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(
                response,
                new JsonSerializerSettings { Formatting = Formatting.Indented })
                );
        }

        private async Task<ClaimsIdentity> GetIdentityAsync(string emailOrPassword, string password)
        {
            User foundUser = await manager.FindByEmailAsync(emailOrPassword);
            foundUser ??= await manager.Users.FirstOrDefaultAsync(
                user => user.PhoneNumber.Equals(emailOrPassword)
                );

            if (foundUser != null)
            {
                bool isPasswordValid = await manager.CheckPasswordAsync(foundUser, password);

                if (isPasswordValid)
                {
                    IList<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, foundUser.Email)
                    };
                    ClaimsIdentity claimsIdentity =
                        new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);

                    foundUser.LastSeen = DateTime.Now;
                    await manager.UpdateAsync(foundUser);

                    return claimsIdentity;
                }
            }

            return null;
        }

        [HttpGet]
        [Route("userdata")]
        public async Task<IActionResult> Get()
        {
            string email = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email).Value;

            User foundUser = await manager.FindByEmailAsync(email);
            if (foundUser != null)
            {
                return Ok(new
                {
                    name = foundUser.UserName,
                    email = foundUser.Email,
                    phone = foundUser.PhoneNumber,
                    registered = foundUser.Registered,
                    avatarSrc = (await imageService.GetAvatar(foundUser.Email)).Link
                });
            }
            else
            {
                return BadRequest("email is not valid");
            }
        }

        [HttpPost]
        [Route("logout")]
        public async Task Logout()
        {
            string email = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email).Value;
            User foundUser = await manager.FindByEmailAsync(email);

            foundUser.LastSeen = DateTime.Now;
            await manager.UpdateAsync(foundUser);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using VironIT_Social_network_server.WEB.Identity.JWT;
using System.Text;

namespace VironIT_Social_network_server.WEB.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private UserManager<IdentityUser> manager;
        public AccountsController(UserManager<IdentityUser> manager)
        {
            this.manager = manager;
        }

        [HttpPost("/token")]
        public async Task Token()
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];

            ClaimsIdentity identity = await GetIdentityAsync(username, password);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
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
                username = identity.Name
            };

            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(
                response, 
                new JsonSerializerSettings { Formatting = Formatting.Indented })
                );
        }

        private async Task<ClaimsIdentity> GetIdentityAsync(string username, string password)
        {
            IdentityUser person = await manager.FindByNameAsync(username);

            if (person != null)
            {
                bool isPasswordValid = await manager.CheckPasswordAsync(person, password);

                IList<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Email)
                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}
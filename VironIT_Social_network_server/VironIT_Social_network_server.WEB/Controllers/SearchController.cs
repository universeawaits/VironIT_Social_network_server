using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.BLL.Services.Interface;
using VironIT_Social_network_server.WEB.Identity;
using VironIT_Social_network_server.WEB.ViewModels;


namespace VironIT_Social_network_server.WEB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private UserManager<User> manager;
        private IContactService contactService;
        private IImageService imageService;
        private IMapper mapper;

        public SearchController(
            UserManager<User> manager, IContactService contactService, 
            IMapper mapper, IImageService imageService
            )
        {
            this.manager = manager;
            this.contactService = contactService;
            this.mapper = mapper;
            this.imageService = imageService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<ContactProfileModel>> SearchByEmailOrPhone(
            [FromQuery(Name = "emailOrPhone")] string emailOrPhone
            )
        {
            IEnumerable<BlockDTO> blockedForMe = await contactService.GetBlocksForAsync(
                (await manager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email))).Id
                );
            IEnumerable<User> remainUsers = manager.Users
                .Where(
                    user => user.Email.StartsWith(emailOrPhone) ||
                    user.PhoneNumber.StartsWith(emailOrPhone)
                    );
            remainUsers = remainUsers
                .Where(
                    user => blockedForMe.All(
                        block => !block.BlockingUserId.Equals(user.Id))
                    );

            return remainUsers != null ? await ToProfile(remainUsers) : null;
        }

        [HttpGet]
        public async Task<ContactProfileModel> SearchByFullEmail([FromQuery(Name = "email")] string email)
        {
            string currUserId = (await manager.FindByEmailAsync(
                User.FindFirstValue(ClaimTypes.Email))).Id;
            ContactProfileModel found = await ToProfile(await manager.FindByEmailAsync(email), currUserId);

            return found;
        }

        private async Task<IEnumerable<ContactProfileModel>> ToProfile(IEnumerable<User> users)
        {
            ICollection<ContactProfileModel> profiles = new List<ContactProfileModel>();
            string currUserId = (await manager.FindByEmailAsync(
                User.FindFirstValue(ClaimTypes.Email))).Id;

            foreach (User user in users)
            {
                profiles.Add(await ToProfile(user, currUserId));
            }

            return profiles;
        }

        private async Task<ContactProfileModel> ToProfile(User user, string currUserId)
        {
            ContactProfileModel profile = new ContactProfileModel
            {
                IsBlocked = await contactService.IsBlocked(currUserId, user.Id),
                IsContact = await contactService.IsContactedAsync(currUserId, user.Id),
                IsOnline = user.IsOnline,
                LastSeen = user.LastSeen,
                Pseudonym = await contactService.GetPseudonymRawAsync(user.Id),
                User = mapper.Map<User, UserProfileModel>(user)
            };
            AvatarDTO avatar = await imageService.GetMediumAvatar(user.Email);
            profile.User.Avatar = avatar?.Link;

            return profile;
        }
    }
}
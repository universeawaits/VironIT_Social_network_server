using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using VironIT_Social_network_server.BLL.Services.Interface;
using VironIT_Social_network_server.WEB.Identity;
using VironIT_Social_network_server.WEB.ViewModel;


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

        public SearchController(UserManager<User> manager, IContactService contactService, IMapper mapper, IImageService imageService)
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
        {/*
            IEnumerable<BlockDTO> blockedForMe = await contactService.GetBlocksFor(
                User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email).Value
                );
            IEnumerable<User> remainUsers = manager.Users.Where(
                    user => blockedForMe.All(
                        block => !block.BlockedUserId.Equals(user.Id)
                        )).Where(
                            user => user.Email.StartsWith(emailOrPhone) || 
                            user.PhoneNumber.StartsWith(emailOrPhone)
                            );*/
            IEnumerable<ContactProfileModel> profiles = await ToProfile(/*remainUsers*/
                manager.Users.Where(user => user.Email.StartsWith(emailOrPhone) ||
                            user.PhoneNumber.StartsWith(emailOrPhone)).AsEnumerable());

            return profiles;
        }

        private async Task<IEnumerable<ContactProfileModel>> ToProfile(IEnumerable<User> users)
        {
            ICollection<ContactProfileModel> profiles = new List<ContactProfileModel>();
            string currUserId = (await manager.FindByEmailAsync(
                User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email).Value))
                .Id;

            foreach (User user in users)
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
                profile.User.Avatar = (await imageService.GetLargeAvatar(user.Email)).Link;
                profiles.Add(profile);
            }

            return profiles;
        }
    }
}
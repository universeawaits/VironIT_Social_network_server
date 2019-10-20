using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.BLL.Services;
using VironIT_Social_network_server.BLL.Services.Interface;
using VironIT_Social_network_server.WEB.Identity;
using VironIT_Social_network_server.WEB.ViewModels;


namespace VironIT_Social_network_server.WEB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private UserManager<User> manager;
        private IContactService contactService;
        private IImageService imageService;
        private IMapper mapper;

        public ContactsController(
            UserManager<User> manager,
            IContactService contactService, 
            IMapper mapper, 
            IImageService imageService)
        {
            this.manager = manager;
            this.contactService = contactService;
            this.imageService = imageService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<ContactProfileModel>> GetAllContacts()
        {
            IEnumerable<ContactDTO> contacts = await contactService.GetContacts(
                (await manager.FindByEmailAsync(
                    User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email).Value
                    )).Id);
            return await ToProfileModel(contacts);
        }

        private async Task<IEnumerable<ContactProfileModel>> ToProfileModel(IEnumerable<ContactDTO> contacts)
        {
            ICollection<ContactProfileModel> profiles = new List<ContactProfileModel>();

            foreach (ContactDTO contact in contacts)
            {
                User contacted = await manager.FindByIdAsync(contact.ContactedUserId);
                ContactProfileModel profile = new ContactProfileModel 
                {
                    IsBlocked = false,
                    IsContact = true,
                    IsOnline = contacted.IsOnline,
                    LastSeen = contacted.LastSeen,
                    Pseudonym = await contactService.GetPseudonymRawAsync(contacted.Id),
                    User = mapper.Map<User, UserProfileModel>(contacted)
                };
                profile.User.Avatar = (await imageService.GetMediumAvatar(contacted.Email)).Link;
                profiles.Add(profile);
            }

            return profiles;
        }

        [HttpPost]
        [Route("addContact")]
        public async Task AddContact([FromBody] ContactModel contact)
        {
            await contactService.AddContactAsync(mapper.Map<ContactModel, ContactDTO>(contact));
        }

        [HttpPost]
        [Route("removeContact")]
        public async Task RemoveContact([FromBody] ContactModel contact)
        {
            await contactService.RemoveContactAsync(mapper.Map<ContactModel, ContactDTO>(contact));
        }

        [HttpPost]
        [Route("block")]
        public async Task AddBlock([FromBody] BlockModel block)
        {
            await contactService.AddBlockAsync(mapper.Map<BlockModel, BlockDTO>(block));
        }

        [HttpPost]
        [Route("unblock")]
        public async Task Unblock([FromBody] BlockModel block)
        {
            await contactService.UnblockAsync(mapper.Map<BlockModel, BlockDTO>(block));
        }

        [HttpPost]
        [Route("setPseudonym")]
        public async Task SetPseudonym([FromBody] PseudonymModel pseudonym)
        {
            if (pseudonym.PseudonymRaw.Trim().Equals(""))
            {
                await contactService.RemovePseudonymAsync(mapper.Map<PseudonymModel, PseudonymDTO>(pseudonym));
            }
            else
            {
                await contactService.SetPseudonymAsync(mapper.Map<PseudonymModel, PseudonymDTO>(pseudonym));
            }
        }
    }
}
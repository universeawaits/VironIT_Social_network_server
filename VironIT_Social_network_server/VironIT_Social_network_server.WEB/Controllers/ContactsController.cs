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
                AvatarDTO avatar = await imageService.GetMediumAvatar(contacted.Email);
                profile.User.Avatar = avatar?.Link;
                profiles.Add(profile);
            }

            return profiles;
        }
        
        [HttpPost]
        [Route("blocks")]
        public async Task SetContact([FromBody] BlockModel block)
        {
            BlockDTO blockDTO = mapper.Map<BlockModel, BlockDTO>(block);

            if (await contactService.IsBlocked(blockDTO.BlockingUserId, blockDTO.BlockedUserId))
            {
                await contactService.UnblockAsync(mapper.Map<BlockModel, BlockDTO>(block));
            }
            else
            {
                await contactService.AddBlockAsync(mapper.Map<BlockModel, BlockDTO>(block));
            }
        }

        [HttpPost]
        [Route("pseudonyms")]
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


        [HttpPost]
        public async Task SetContact([FromBody] ContactModel contact)
        {
            ContactDTO contactDTO = mapper.Map<ContactModel, ContactDTO>(contact);

            if (await contactService.IsContactedAsync(contactDTO.ContactingUserId, contactDTO.ContactedUserId))
            {
                await contactService.RemoveContactAsync(contactDTO);
            }
            else
            {
                await contactService.AddContactAsync(contactDTO);
            }
        }
    }
}
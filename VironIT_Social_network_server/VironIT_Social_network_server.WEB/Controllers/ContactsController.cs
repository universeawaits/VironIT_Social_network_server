using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.BLL.Services;
using VironIT_Social_network_server.BLL.Services.Interface;
using VironIT_Social_network_server.WEB.ViewModel;


namespace VironIT_Social_network_server.WEB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private IContactService contactService;
        private IMapper mapper;

        public ContactsController(IContactService contactService, IMapper mapper)
        {
            this.contactService = contactService;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("addContact")]
        public async Task AddContact([FromBody] ContactModel contact)
        {
            Console.WriteLine(mapper.Map<ContactModel, ContactDTO>(contact).ContactingUserId);
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
        [Route("setPseudo")]
        public async Task SetPseudonym([FromBody] PseudonymModel pseudonym)
        {
            await contactService.SetPseudonymAsync(mapper.Map<PseudonymModel, PseudonymDTO>(pseudonym));
        }

        [HttpPost]
        [Route("removePseudo")]
        public async Task RemovePseudonym([FromBody] PseudonymModel pseudonym)
        {
            await contactService.RemovePseudonymAsync(mapper.Map<PseudonymModel, PseudonymDTO>(pseudonym));
        }
    }
}
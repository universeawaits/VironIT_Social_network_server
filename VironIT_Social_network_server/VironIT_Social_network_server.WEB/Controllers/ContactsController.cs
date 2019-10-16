using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public ContactsController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        public async Task AddContact([FromBody] ContactDTO contact)
        {
            await contactService.AddContactAsync(contact);
        }

        public async Task RemoveContact([FromBody] ContactDTO contact)
        {
            await contactService.RemoveContactAsync(contact);
        }

        public async Task AddBlock([FromBody] BlockDTO block)
        {
            await contactService.AddBlockAsync(block);
        }

        public async Task Unblock([FromBody] BlockDTO block)
        {
            await contactService.UnblockAsync(block);
        }

        public async Task SetPseudonym([FromBody] PseudonymDTO pseudonym)
        {
            await contactService.SetPseudonymAsync(pseudonym);
        }

        public async Task RemovePseudonym([FromBody] PseudonymDTO pseudonym)
        {
            await contactService.RemovePseudonymAsync(pseudonym);
        }
    }
}
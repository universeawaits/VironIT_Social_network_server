using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.BLL.Services.Interface;

namespace VironIT_Social_network_server.WEB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private IMessageService messageService;

        private int historyMessagesCount = 100;

        public MessagesController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        // with limit of 100 messages
        [HttpGet]
        [Route("history")]
        public async Task<IEnumerable<MessageDTO>> GetMessagesHistory([FromQuery(Name = "withEmail")] string withEmail)
        {
            return await messageService.GetMessagesHistoryAsync(
                User.FindFirst(ClaimTypes.Email).Value, withEmail, historyMessagesCount);
        }
    }
}
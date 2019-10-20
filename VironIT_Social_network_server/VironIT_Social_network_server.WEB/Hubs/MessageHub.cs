using AutoMapper;

using Microsoft.AspNetCore.SignalR;

using System.Threading.Tasks;
using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.BLL.Services.Interface;


namespace VironIT_Social_network_server.WEB.SignalR
{
    public class MessageHub : Hub
    {
        private IMessageService messageService;
        private IMapper mapper;

        public MessageHub(IMapper mapper, IMessageService messageService)
        {
            this.mapper = mapper;
            this.messageService = messageService;
        }

        public async Task SendMessage(MessageModel message)
        {
            MessageDTO messDTO = mapper.Map<MessageModel, MessageDTO>(message);
            await messageService.AddMessageAsync(messDTO);
            await Clients.Users(Context.UserIdentifier, message.ToEmail).SendAsync("messageReceived", message);
        }
    }
}

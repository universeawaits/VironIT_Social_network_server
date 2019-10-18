using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.BLL.Services.Interface;
using VironIT_Social_network_server.DAL.Context;
using VironIT_Social_network_server.DAL.Model;
using VironIT_Social_network_server.DAL.UnitOfWork;


namespace VironIT_Social_network_server.BLL.Services
{
    public class MessageService : IMessageService
    {
        private IUnitOfWork<MessageContext> unit;
        private IMapper mapper;

        public MessageService(IUnitOfWork<MessageContext> unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public async Task AddMessageAsync(MessageDTO message)
        {
            await unit.Repository<Message>().CreateAsync(
                mapper.Map<MessageDTO, Message>(message)
                );
            await unit.SaveAsync();
        }

        public async Task ClearMessagesHistoryAsync(string fromUserEmail, string toUserEmail)
        {
            IQueryable<Message> toDelete = await GetMessageHistoryAsync(fromUserEmail, toUserEmail);
            foreach (Message message in toDelete)
            {
                await unit.Repository<Message>().DeleteAsync(message.Id);
            }

            await unit.SaveAsync();
        }

        public async Task<IEnumerable<MessageDTO>> GetMessagesHistoryAsync(
            string fromUserEmail, string toUserEmail, int messagesCount)
        {
            return mapper.Map<IEnumerable<Message>, IEnumerable<MessageDTO>>(
                await GetMessageHistoryAsync(fromUserEmail, toUserEmail)).AsEnumerable().Take(messagesCount);
        }

        private async Task<IQueryable<Message>> GetMessageHistoryAsync(
            string fromUserEmail, string toUserEmail)
        {
            return unit.Repository<Message>().GetList(message =>
                (message.FromEmail.Equals(fromUserEmail) && message.ToEmail.Equals(toUserEmail)) ||
                (message.FromEmail.Equals(toUserEmail) && message.ToEmail.Equals(fromUserEmail))
                );
        }
    }
}

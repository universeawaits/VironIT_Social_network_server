using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VironIT_Social_network_server.BLL.DTO;

namespace VironIT_Social_network_server.BLL.Services.Interface
{
    public interface IMessageService
    {
        // with limit of 100 messages
        Task<IEnumerable<MessageDTO>> GetMessagesHistoryAsync(string fromUserEmail, string toUserEmail, int messagesCount);
        Task AddMessageAsync(MessageDTO message);
        Task ClearMessagesHistoryAsync(string fromUserEmail, string toUserEmail);
    }
}

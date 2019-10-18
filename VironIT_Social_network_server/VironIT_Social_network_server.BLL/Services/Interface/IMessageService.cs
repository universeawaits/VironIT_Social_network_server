using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VironIT_Social_network_server.BLL.DTO;

namespace VironIT_Social_network_server.BLL.Services.Interface
{
    public interface IMessageService
    {
        Task AddMessageAsync(MessageDTO message);
    }
}

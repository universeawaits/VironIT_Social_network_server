using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

using System.Threading.Tasks;

using VironIT_Social_network_server.WEB.Identity;


namespace VironIT_Social_network_server.WEB.SignalR
{
    public class MessageHub : Hub
    {
        private UserManager<User> manager;


        public MessageHub(UserManager<User> manager)
        {
            this.manager = manager;
        }

        public async Task SendMessage(Message message)
        {
            await Clients.User(Context.UserIdentifier).SendAsync("messageReceived", message);
        }
    }
}

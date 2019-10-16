using System;
using System.Collections.Generic;
using System.Text;

namespace VironIT_Social_network_server.BLL.Services.Interface
{
    interface IContactService
    {
        void ContactUser(string contactingUserId, string contactedUserId);
        void RemoveContact(string contactingUserId, string contactedUserId);
        void BlockUser(string blockingUserId, string blockedUserId);
        void UnblockUser(string blockingUserId, string blockedUserId);
    }
}

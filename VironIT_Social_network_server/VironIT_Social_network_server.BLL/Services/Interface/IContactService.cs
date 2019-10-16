using System.Threading.Tasks;
using VironIT_Social_network_server.BLL.DTO;

namespace VironIT_Social_network_server.BLL.Services.Interface
{
    interface IContactService
    {
        Task AddContactAsync(ContactDTO contact);
        Task RemoveContactAsync(ContactDTO contact);
        Task AddBlockAsync(BlockDTO block);
        Task UnblockUserAsync(BlockDTO block);
        Task SetPseudonymAsync(PseudonymDTO pseudonym);
        Task RemovePseudonymAsync(PseudonymDTO pseudonym);
    }
}

using System.Threading.Tasks;

using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.DAL.Context;
using VironIT_Social_network_server.DAL.UnitOfWork;

namespace VironIT_Social_network_server.BLL.Services.Interface
{
    interface IContactService
    {
        IUnitOfWork<ContactContext> Unit { get; }

        Task AddContactAsync(ContactDTO contact);
        Task RemoveContactAsync(ContactDTO contact);
        Task AddBlockAsync(BlockDTO block);
        Task UnblockUserAsync(BlockDTO block);
        Task SetPseudonymAsync(PseudonymDTO pseudonym);
        Task RemovePseudonymAsync(PseudonymDTO pseudonym);
    }
}

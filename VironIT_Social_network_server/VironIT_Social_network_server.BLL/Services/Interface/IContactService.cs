using System.Collections.Generic;
using System.Threading.Tasks;

using VironIT_Social_network_server.BLL.DTO;


namespace VironIT_Social_network_server.BLL.Services.Interface
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDTO>> GetContacts(string contactingUserId);
        Task AddContactAsync(ContactDTO contact);
        Task RemoveContactAsync(ContactDTO contact);
        Task<bool> IsContacted(string contactingUserId, string probContactedUserId);
        Task AddBlockAsync(BlockDTO block);
        Task UnblockAsync(BlockDTO block);
        Task<bool> IsBlocked(string blockingUserId, string probBlockedUserId);
        Task<IEnumerable<BlockDTO>> GetBlocksForAsync(string blockedUserId);
        Task SetPseudonymAsync(PseudonymDTO pseudonym);
        Task RemovePseudonymAsync(PseudonymDTO pseudonym);
        Task<string> GetPseudonymRawAsync(string pseudoForUserId);
    }
}

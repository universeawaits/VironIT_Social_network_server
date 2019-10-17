using AutoMapper;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.BLL.Services.Interface;
using VironIT_Social_network_server.DAL.Context;
using VironIT_Social_network_server.DAL.Model;
using VironIT_Social_network_server.DAL.UnitOfWork;


namespace VironIT_Social_network_server.BLL.Services
{
    public class ContactService : IContactService
    {
        private IUnitOfWork<ContactContext> unit;
        private IMapper mapper;

        public ContactService(IUnitOfWork<ContactContext> unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public async Task AddBlockAsync(BlockDTO block)
        {
            await unit.Repository<Block>().CreateAsync(
                mapper.Map<BlockDTO, Block>(block)
                );
            await unit.SaveAsync();
        }

        public async Task AddContactAsync(ContactDTO contact)
        {
            await unit.Repository<Contact>().CreateAsync(
                mapper.Map<ContactDTO, Contact>(contact)
                );
            await unit.SaveAsync();
        }

        public async Task RemoveContactAsync(ContactDTO contact)
        {
            Contact found = await unit.Repository<Contact>().GetEntityByFilter(
                _contact => 
                    _contact.ContactingUserId.Equals(contact.ContactingUserId) &&
                    _contact.ContactedUserId.Equals(contact.ContactedUserId)
                );

            if (found != null)
            {
                await unit.Repository<Contact>().DeleteAsync(found.Id);
                await unit.SaveAsync();
            }
        }

        public async Task RemovePseudonymAsync(PseudonymDTO pseudonym)
        {
            Pseudonym found = await unit.Repository<Pseudonym>().GetEntityByFilter(
                _pseudonym =>
                    _pseudonym.PseudoFromUserId.Equals(pseudonym.PseudoFromUserId) &&
                    _pseudonym.PseudoForUserId.Equals(pseudonym.PseudoForUserId)
                );

            if (found != null)
            {
                await unit.Repository<Pseudonym>().DeleteAsync(found.Id);
                await unit.SaveAsync();
            }
        }

        public async Task SetPseudonymAsync(PseudonymDTO pseudonym)
        {
            await unit.Repository<Pseudonym>().CreateAsync(
                mapper.Map<PseudonymDTO, Pseudonym>(pseudonym)
                );
            await unit.SaveAsync();
        }

        public async Task UnblockAsync(BlockDTO block)
        {
            Block found = await unit.Repository<Block>().GetEntityByFilter(
                _block =>
                    _block.BlockingUserId.Equals(block.BlockingUserId) &&
                    _block.BlockedUserId.Equals(block.BlockedUserId)
                );
            if (found != null)
            {
                await unit.Repository<Block>().DeleteAsync(found.Id);
                await unit.SaveAsync();
            }
        }

        public async Task<IEnumerable<ContactDTO>> GetContacts(string contactingUserId)
        {
            return mapper.Map<IEnumerable<Contact>, IEnumerable<ContactDTO>>(
                unit.Repository<Contact>().GetList(
                    contact => contact.ContactingUserId.Equals(contactingUserId)
                    ));
        }

        public async Task<string> GetPseudonymRawAsync(string pseudoForUserId)
        {
            Pseudonym found = await unit.Repository<Pseudonym>()
                .GetEntityByFilter(
                    _pseudonym => _pseudonym.PseudoForUserId.Equals(pseudoForUserId)
                    );
            return found != null ? found.PseudonymRaw : "";
        }

        public async Task<IEnumerable<BlockDTO>> GetBlocksForAsync(string blockedUserId)
        {
            return mapper.Map<IEnumerable<Block>, IEnumerable<BlockDTO>>(
                unit.Repository<Block>()
                    .GetList(block => block.BlockedUserId.Equals(blockedUserId)));
        }

        public async Task<bool> IsContactedAsync(string contactingUserId, string probContactedUserId)
        {
            Contact c = await unit.Repository<Contact>()
                .GetEntityByFilter(
                contact => contact.ContactedUserId.Equals(probContactedUserId) &&
                contact.ContactingUserId.Equals(contactingUserId));
            return c != null;
        }

        public async Task<bool> IsBlocked(string blockingUserId, string probBlockedUserId)
        {
            return (await unit.Repository<Block>()
                .GetEntityByFilter(
                block => block.BlockedUserId.Equals(probBlockedUserId) &&
                block.BlockingUserId.Equals(blockingUserId))) != null;
        }
    }
}

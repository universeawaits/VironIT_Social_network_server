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
        public IUnitOfWork<ContactContext> Unit { get; }
        private IMapper mapper;

        public ContactService(IUnitOfWork<ContactContext> unit, IMapper mapper)
        {
            Unit = unit;
            this.mapper = mapper;
        }

        public async Task AddBlockAsync(BlockDTO block)
        {
            await Unit.Repository<Block>().CreateAsync(
                mapper.Map<BlockDTO, Block>(block)
                );
            await Unit.SaveAsync();
        }

        public async Task AddContactAsync(ContactDTO contact)
        {
            await Unit.Repository<Contact>().CreateAsync(
                mapper.Map<ContactDTO, Contact>(contact)
                );
            await Unit.SaveAsync();
        }

        public async Task RemoveContactAsync(ContactDTO contact)
        {
            Contact found = await Unit.Repository<Contact>().GetEntityByFilter(
                _contact => 
                    _contact.ContactingUserId.Equals(contact.ContactingUserId) &&
                    _contact.ContactedUserId.Equals(contact.ContactedUserId)
                );
            await Unit.Repository<Contact>().DeleteAsync(found.Id);
            await Unit.SaveAsync();
        }

        public async Task RemovePseudonymAsync(PseudonymDTO pseudonym)
        {
            Pseudonym found = await Unit.Repository<Pseudonym>().GetEntityByFilter(
                _pseudonym =>
                    _pseudonym.PseudoFromUserId.Equals(pseudonym.PseudoFromUserId) &&
                    _pseudonym.PseudoForUserId.Equals(pseudonym.PseudoForUserId)
                );
            await Unit.Repository<Pseudonym>().DeleteAsync(found.Id);
            await Unit.SaveAsync();
        }

        public async Task SetPseudonymAsync(PseudonymDTO pseudonym)
        {
            await Unit.Repository<Pseudonym>().CreateAsync(
                mapper.Map<PseudonymDTO, Pseudonym>(pseudonym)
                );
            await Unit.SaveAsync();
        }

        public async Task UnblockAsync(BlockDTO block)
        {
            Block found = await Unit.Repository<Block>().GetEntityByFilter(
                _block =>
                    _block.BlockingUserId.Equals(block.BlockingUserId) &&
                    _block.BlockedUserId.Equals(block.BlockedUserId)
                );
            await Unit.Repository<Block>().DeleteAsync(found.Id);
            await Unit.SaveAsync();
        }
    }
}

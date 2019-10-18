using AutoMapper;

using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.DAL.Model;


namespace VironIT_Social_network_server.BLL
{
    public class BLLMapperProfile : Profile
    {
        public BLLMapperProfile()
        {
            CreateMap<Avatar, AvatarDTO>();
            CreateMap<AvatarDTO, Avatar>();
            CreateMap<Contact, ContactDTO>();
            CreateMap<ContactDTO, Contact>();
            CreateMap<Block, BlockDTO>();
            CreateMap<BlockDTO, Block>();
            CreateMap<PseudonymDTO, Pseudonym>();
            CreateMap<Pseudonym, PseudonymDTO>();
            CreateMap<Message, MessageDTO>();
            CreateMap<MessageDTO, Message>();
        }
    }
}

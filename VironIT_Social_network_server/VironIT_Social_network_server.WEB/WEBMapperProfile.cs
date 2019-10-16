using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.WEB.ViewModel;

namespace VironIT_Social_network_server.WEB
{
    public class WEBMapperProfile : Profile
    {
        public WEBMapperProfile()
        {
            CreateMap<ContactDTO, ContactModel>();
            CreateMap<ContactModel, ContactDTO>();
            CreateMap<BlockDTO, BlockModel>();
            CreateMap<BlockModel, BlockDTO>();
            CreateMap<PseudonymDTO, PseudonymModel>();
            CreateMap<PseudonymModel, PseudonymDTO>();
        }
    }
}

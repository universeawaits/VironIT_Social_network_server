using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.BLL.Services.Interface;
using VironIT_Social_network_server.WEB.Identity;
using VironIT_Social_network_server.WEB.ViewModel;

namespace VironIT_Social_network_server.WEB
{
    public class WEBMapperProfile : Profile
    {
        private UserManager<User> manager;

        public WEBMapperProfile(UserManager<User> manager)
        {
            this.manager = manager;

            //CreateMap<ContactDTO, ContactModel>();
            CreateMap<ContactModel, ContactDTO>().ForMember(
                dest => dest.ContactedUserId,
                options => options.MapFrom(src => manager.FindByEmailAsync(src.ContactedUserEmail).Id)
                ).ForMember(
                dest => dest.ContactingUserId,
                options => options.MapFrom(src => manager.FindByEmailAsync(src.ContactingUserEmail).Id)
                );
            //CreateMap<BlockDTO, BlockModel>();
            CreateMap<BlockModel, BlockDTO>().ForMember(
                dest => dest.BlockedUserId,
                options => options.MapFrom(src => manager.FindByEmailAsync(src.BlockedUserEmail).Id)
                ).ForMember(
                dest => dest.BlockingUserId,
                options => options.MapFrom(src => manager.FindByEmailAsync(src.BlockingUserEmail).Id)
                );
            //CreateMap<PseudonymDTO, PseudonymModel>();
            CreateMap<PseudonymModel, PseudonymDTO>().ForMember(
                dest => dest.PseudoFromUserId,
                options => options.MapFrom(src => manager.FindByEmailAsync(src.PseudoFromUserEmail).Id)
                ).ForMember(
                dest => dest.PseudoForUserId,
                options => options.MapFrom(src => manager.FindByEmailAsync(src.PseudoForUserEmail).Id)
                );
        }
    }
}

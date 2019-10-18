using AutoMapper;

using Microsoft.AspNetCore.Identity;

using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.WEB.Identity;
using VironIT_Social_network_server.WEB.SignalR;
using VironIT_Social_network_server.WEB.ViewModel;


namespace VironIT_Social_network_server.WEB
{
    public class WEBMapperProfile : Profile
    {
        private UserManager<User> manager;

        public WEBMapperProfile(UserManager<User> manager)
        {
            this.manager = manager;

            CreateMap<User, UserProfileModel>().ForMember(
                dest => dest.Name,
                options => options.MapFrom(src => manager.FindByIdAsync(src.Id).Result.UserName)
                ).ForMember(
                dest => dest.Phone,
                options => options.MapFrom(src => manager.FindByIdAsync(src.Id).Result.PhoneNumber)
                );

            //CreateMap<ContactDTO, ContactModel>();

            CreateMap<ContactModel, ContactDTO>().ForMember(
                dest => dest.ContactedUserId,
                options => options.MapFrom(src => manager.FindByEmailAsync(src.ContactedUserEmail).Result.Id)
                ).ForMember(
                dest => dest.ContactingUserId,
                options => options.MapFrom(src => manager.FindByEmailAsync(src.ContactingUserEmail).Result.Id)
                );
            //CreateMap<BlockDTO, BlockModel>();
            CreateMap<BlockModel, BlockDTO>().ForMember(
                dest => dest.BlockedUserId,
                options => options.MapFrom(src => manager.FindByEmailAsync(src.BlockedUserEmail).Result.Id)
                ).ForMember(
                dest => dest.BlockingUserId,
                options => options.MapFrom(src => manager.FindByEmailAsync(src.BlockingUserEmail).Result.Id)
                );
            //CreateMap<PseudonymDTO, PseudonymModel>();
            CreateMap<PseudonymModel, PseudonymDTO>().ForMember(
                dest => dest.PseudoFromUserId,
                options => options.MapFrom(src => manager.FindByEmailAsync(src.PseudoFromUserEmail).Result.Id)
                ).ForMember(
                dest => dest.PseudoForUserId,
                options => options.MapFrom(src => manager.FindByEmailAsync(src.PseudoForUserEmail).Result.Id)
                );
            CreateMap<MessageModel, MessageDTO>();
        }
    }
}

using AutoMapper;

using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.DAL.Model;


namespace VironIT_Social_network_server.BLL
{
    public class BLLMapperProfile : Profile
    {
        public BLLMapperProfile()
        {
            CreateMap<Image, ImageDTO>();
            CreateMap<ImageDTO, Image>();
        }
    }
}

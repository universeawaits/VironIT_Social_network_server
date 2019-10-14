using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.DAL.Context;
using VironIT_Social_network_server.DAL.UnitOfWork;

namespace VironIT_Social_network_server.BLL.Services.Interface
{
    public interface IImageService : IService<IUnitOfWork<ImageContext>>
    {
        Task<ImageDTO> Resize(int id, uint width, uint height);
        //Task<ImageDTO> Resize(ImageDTO image, uint width, uint height);
        Task<ImageDTO> Compress(int id, uint width, uint height);
        //Task<ImageDTO> Compress(ImageDTO image, uint width, uint height);
    }
}

using System.IO;
using System.Threading.Tasks;

using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.DAL.Context;
using VironIT_Social_network_server.DAL.UnitOfWork;


namespace VironIT_Social_network_server.BLL.Services.Interface
{
    public interface IImageService : IService<ImageDTO>
    {
        IUnitOfWork<ImageContext> Unit { get; }

        Task<Stream> ReadAsync(ImageDTO image);
        Task AddAvatar(Stream image, string userEmail);
        Task<ImageDTO> ResizeAsync(ImageDTO image, uint width, uint height);
        Task<ImageDTO> CompressAsync(ImageDTO image, uint width, uint height);
    }
}

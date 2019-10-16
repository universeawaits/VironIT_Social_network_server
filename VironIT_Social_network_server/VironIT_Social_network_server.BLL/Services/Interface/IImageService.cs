using System.IO;
using System.Threading.Tasks;

using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.DAL.Context;
using VironIT_Social_network_server.DAL.UnitOfWork;


namespace VironIT_Social_network_server.BLL.Services.Interface
{
    public interface IImageService
    {
        IUnitOfWork<ImageContext> Unit { get; }

        Task AddAvatarAsync(AvatarDTO image);
        Task<AvatarDTO> GetAvatar(string userEmail);
        Task UpdateAvatarAsync(Stream image, string userEmail);
    }
}

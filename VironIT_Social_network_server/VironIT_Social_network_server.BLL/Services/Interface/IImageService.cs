using System.IO;
using System.Threading.Tasks;

using VironIT_Social_network_server.BLL.DTO;


namespace VironIT_Social_network_server.BLL.Services.Interface
{
    public interface IImageService
    {
        Task AddAvatarAsync(AvatarDTO image);
        Task<AvatarDTO> GetLargeAvatar(string userEmail);
        Task UpdateAvatarAsync(Stream image, string userEmail);
    }
}

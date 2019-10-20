using System.IO;
using System.Threading.Tasks;

using VironIT_Social_network_server.BLL.DTO;


namespace VironIT_Social_network_server.BLL.Services.Interface
{
    public interface IAudioService
    {
        Task<AudioDTO> UploadAudioAsync(Stream audio, string userEmail);
    }
}

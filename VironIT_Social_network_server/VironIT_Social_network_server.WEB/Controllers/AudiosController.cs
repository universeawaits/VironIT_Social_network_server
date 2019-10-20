using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.BLL.Services.Interface;


namespace VironIT_Social_network_server.WEB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AudiosController : ControllerBase
    {
        private IAudioService audioService;

        public AudiosController(IAudioService audioService)
        {
            this.audioService = audioService;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAvatar([FromForm(Name = "file")] IFormFile audio)
        {
            if (audio != null)
            {
                AudioDTO uploadedAudio = await audioService.UploadAudioAsync(
                    audio.OpenReadStream(),
                    User.FindFirstValue(ClaimTypes.Email)
                    );
                return Created("/audios", uploadedAudio);
            }

            return BadRequest();
        }
    }
}
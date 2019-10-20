using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.BLL.Services.Interface;


namespace VironIT_Social_network_server.WEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagesController : ControllerBase
    {
        private IImageService imageService;

        public ImagesController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpPost]
        [Route("avatars")]
        public async Task UpdateAvatar([FromForm(Name = "file")] IFormFile image)
        {
            if (image != null)
            {
                await imageService.UpdateAvatarAsync(
                    image.OpenReadStream(),
                    User.FindFirstValue(ClaimTypes.Email)
                    );
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm(Name = "file")] IFormFile image)
        {
            if (image != null)
            {
                ImageDTO uploadedImage = await imageService.UploadImageAsync(
                    image.OpenReadStream(),
                    User.FindFirstValue(ClaimTypes.Email)
                    );
                return Created("/images", uploadedImage);
            }

            return BadRequest();
        }
    }
}
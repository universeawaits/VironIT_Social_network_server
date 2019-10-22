using System;
using System.Collections.Generic;
using System.Linq;
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
    public class VideosController : ControllerBase
    {
        private IVideoService videoService;

        public VideosController(IVideoService videoService)
        {
            this.videoService = videoService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadVideo([FromForm(Name = "file")] IFormFile audio)
        {
            if (audio != null)
            {
                VideoDTO uploadedVideo = await videoService.UploadVideoAsync(
                    audio.OpenReadStream(),
                    User.FindFirstValue(ClaimTypes.Email)
                    );
                return Created("/videos", uploadedVideo);
            }

            return BadRequest();
        }
    }
}
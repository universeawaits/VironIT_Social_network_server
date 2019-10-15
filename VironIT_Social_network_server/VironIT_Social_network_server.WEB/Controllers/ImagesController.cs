using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task UpdateAvatar([FromForm(Name = "image")] IFormFile image, [FromForm(Name = "useremail")] string useremail)
        {
            if (image != null)
            {
                await imageService.AddAvatar(image.OpenReadStream(), useremail);
            }
        }
    }
}
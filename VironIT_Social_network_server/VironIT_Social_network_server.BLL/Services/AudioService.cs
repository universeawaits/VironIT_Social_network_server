using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.BLL.Services.Interface;
using VironIT_Social_network_server.DAL.Context;
using VironIT_Social_network_server.DAL.Model;
using VironIT_Social_network_server.DAL.UnitOfWork;

namespace VironIT_Social_network_server.BLL.Services
{
    public class AudioService : IAudioService
    {
        private IUnitOfWork<MediaContext> unit;
        private IMapper mapper;

        private string audiosFolder = @"audios\";

        public AudioService(IUnitOfWork<MediaContext> unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public async Task<AudioDTO> UploadAudioAsync(Stream audio, string userEmail)
        {
            var relAudioPath = @"wwwroot\" + audiosFolder + "\\";
            var fullAudioPath = Path.Combine(Directory.GetCurrentDirectory(), relAudioPath);

            if (!Directory.Exists(fullAudioPath))
            {
                Directory.CreateDirectory(fullAudioPath);
            }

            var uniqueFileName =
                DateTime.Now.ToString("ddmmyyhhmmss") +
                "_" + userEmail + ".mp3";

            using (var fileStream = new FileStream(Path.Combine(fullAudioPath, uniqueFileName), FileMode.Create))
            {
                await audio.CopyToAsync(fileStream);
            }

            var link = "https://localhost:44345/audios/" + uniqueFileName;

            Audio newAudio = new Audio 
            {
                Link = link,
                UserEmail = userEmail
            };
            await unit.Repository<Audio>().CreateAsync(newAudio);
            await unit.SaveAsync();

            return mapper.Map<Audio, AudioDTO>(newAudio);
        }
    }
}

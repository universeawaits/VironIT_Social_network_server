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
    public class VideoService : IVideoService
    {
        private IUnitOfWork<MediaContext> unit;
        private IMapper mapper;

        private string videosFolder = @"videos\";
        private string linkBase;

        public VideoService(IUnitOfWork<MediaContext> unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;

            linkBase = "https://localhost:44345/";
        }

        public async Task<VideoDTO> UploadVideoAsync(Stream video, string userEmail)
        {
            var relVideoPath = @"wwwroot\" + videosFolder + "\\";
            var fullVideoPath = Path.Combine(Directory.GetCurrentDirectory(), relVideoPath);

            if (!Directory.Exists(fullVideoPath))
            {
                Directory.CreateDirectory(fullVideoPath);
            }

            var uniqueFileName =
                DateTime.Now.ToString("ddmmyyhhmmss") +
                "_" + userEmail + ".mp4";

            using (var fileStream = new FileStream(Path.Combine(fullVideoPath, uniqueFileName), FileMode.Create))
            {
                await video.CopyToAsync(fileStream);
            }

            var link = linkBase + "videos/" + uniqueFileName;

            Video newVideo = new Video
            {
                Link = link,
                UserEmail = userEmail
            };
            await unit.Repository<Video>().CreateAsync(newVideo);
            await unit.SaveAsync();

            return mapper.Map<Video, VideoDTO>(newVideo);
        }
    }
}

﻿using AutoMapper;
using ImageProcessor;
using ImageProcessor.Imaging.Formats;

using System.Drawing;
using System.IO;
using System.Threading.Tasks;

using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.BLL.Services.Interface;
using VironIT_Social_network_server.DAL.Context;
using VironIT_Social_network_server.DAL.Model;
using VironIT_Social_network_server.DAL.UnitOfWork;


namespace VironIT_Social_network_server.BLL.Services
{
    public class ImageService : IImageService
    {
        private IUnitOfWork<ImageContext> unit;
        private IMapper mapper;

        private Size largeSize = new Size(250, 250);
        private Size mediumSize = new Size(200, 200);

        private string AvatarFolder = @"images\avatars\";

        public ImageService(IUnitOfWork<ImageContext> unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public async Task AddAvatarAsync(AvatarDTO entity)
        {
            await unit.Repository<Avatar>().CreateAsync(mapper.Map<AvatarDTO, Avatar>(entity));
            await unit.SaveAsync();
        }

        public async Task<AvatarDTO> GetLargeAvatar(string userEmail)
        {
            return mapper.Map<Avatar, AvatarDTO>(
                await unit.Repository<Avatar>().GetEntityByFilter(
                    image => 
                        image.UserEmail.Equals(userEmail) &&
                        image.SizeCategory.Equals(SizeCategory.Large)
                    ));
        }
        public async Task<AvatarDTO> GetMediumAvatar(string userEmail)
        {
            return mapper.Map<Avatar, AvatarDTO>(
                await unit.Repository<Avatar>().GetEntityByFilter(
                    image =>
                        image.UserEmail.Equals(userEmail) &&
                        image.SizeCategory.Equals(SizeCategory.Medium)
                    ));
        }

        public async Task UpdateAvatarAsync(Stream image, string userEmail)
        {
            var largeAvatar = @"wwwroot\" + AvatarFolder + "large";
            var mediumAvatar = @"wwwroot\" + AvatarFolder + "medium";
            var largeAvatarPath = Path.Combine(Directory.GetCurrentDirectory(), largeAvatar);
            var mediumAvatarPath = Path.Combine(Directory.GetCurrentDirectory(), mediumAvatar);

            if (!Directory.Exists(largeAvatarPath))
            {
                Directory.CreateDirectory(largeAvatarPath);
            }
            if (!Directory.Exists(mediumAvatarPath))
            {
                Directory.CreateDirectory(mediumAvatarPath);
            }

            var uniqueFileName = userEmail + ".jpg";

            using (var largeFileStream = new FileStream(Path.Combine(largeAvatarPath, uniqueFileName), FileMode.Create))
            {
                await image.CopyToAsync(largeFileStream);
                await largeFileStream.DisposeAsync();
                Resize(Path.Combine(largeAvatarPath, uniqueFileName), largeSize);
            }

            image.Position = 0;
            using (var mediumFileStream = new FileStream(Path.Combine(mediumAvatarPath, uniqueFileName), FileMode.Create))
            {
                await image.CopyToAsync(mediumFileStream);
                await mediumFileStream.DisposeAsync();
                Resize(Path.Combine(mediumAvatarPath, uniqueFileName), mediumSize);
            }

            var largeLink = "https://localhost:44345/images/avatars/large/" + uniqueFileName;
            var mediumLink = "https://localhost:44345/images/avatars/medium/" + uniqueFileName;

            await UpdateAvatarsLinks(userEmail, largeLink, mediumLink);
        }

        private async Task UpdateAvatarsLinks(string userEmail, string largeLink, string mediumLink)
        {
            Avatar large = await unit.Repository<Avatar>().GetEntityByFilter(
                image => 
                    image.UserEmail.Equals(userEmail) && 
                    image.SizeCategory.Equals(SizeCategory.Large)
                );

            if (large.Link.Equals(""))
            {
                Avatar medium = await unit.Repository<Avatar>().GetEntityByFilter(
                    image => 
                        image.UserEmail.Equals(userEmail) && 
                        image.SizeCategory.Equals(SizeCategory.Medium)
                    );

                large.Link = largeLink;
                medium.Link = mediumLink;

                unit.Repository<Avatar>().Update(large);
                unit.Repository<Avatar>().Update(medium);

                await unit.SaveAsync();
            }
        }

        private void Resize(string path, Size size)
        {
            ISupportedImageFormat format = new JpegFormat();
            byte[] imageBytes = File.ReadAllBytes(path);

            using (var inStream = new MemoryStream(imageBytes))
            {
                using (var outStream = new FileStream(path, FileMode.Create))
                {
                    using (var imageFactory = new ImageFactory(preserveExifData: true))
                    {
                        imageFactory.Load(inStream);

                        Size originalSize = imageFactory.Image.Size;
                        int recSide = originalSize.Width > originalSize.Height ? 
                            originalSize.Height : 
                            originalSize.Width;

                        imageFactory.Format(format)
                            .Crop(
                                new Rectangle(
                                    new Point(0, 0), 
                                    new Size(recSide, recSide)
                                    ))
                            .Resize(size)
                            .Save(outStream);
                    }
                }
            }
        }
    }
}

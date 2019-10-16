using AutoMapper;
using ImageProcessor;
using ImageProcessor.Imaging.Formats;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.BLL.Services.Interface;
using VironIT_Social_network_server.DAL.Context;
using VironIT_Social_network_server.DAL.UnitOfWork;


namespace VironIT_Social_network_server.BLL.Services
{
    public class ImageService : IImageService
    {
        public IUnitOfWork<ImageContext> Unit { get; }
        private IMapper mapper;

        private Size largeSize = new Size(250, 250);
        private Size mediumSize = new Size(200, 200);

        private string AvatarFolder = @"images\avatars\";

        public ImageService(IUnitOfWork<ImageContext> unit, IMapper mapper)
        {
            Unit = unit;
            this.mapper = mapper;
        }

        public async Task AddAsync(ImageDTO entity)
        {
            await Unit.Repository<DAL.Model.Avatar>().CreateAsync(mapper.Map<ImageDTO, DAL.Model.Avatar>(entity));
            await Unit.SaveAsync();
        }

        public async Task<ImageDTO> GetAvatar(string userEmail)
        {
            return mapper.Map<DAL.Model.Avatar, ImageDTO>(
                await Unit.Repository<DAL.Model.Avatar>().GetEntityByFilter(
                    image => image.UserEmail.Equals(userEmail))
                );
        } 

        public async Task AddAvatar(Stream image, string userEmail)
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
            DAL.Model.Avatar large = await Unit.Repository<DAL.Model.Avatar>().GetEntityByFilter(
                image => image.UserEmail.Equals(userEmail) && image.ImageSize.Equals("Large")
                );

            if (large.Link.Equals(""))
            {
                DAL.Model.Avatar medium = await Unit.Repository<DAL.Model.Avatar>().GetEntityByFilter(
                    image => image.UserEmail.Equals(userEmail) && image.ImageSize.Equals("Medium")
                    );

                large.Link = largeLink;
                medium.Link = mediumLink;

                Unit.Repository<DAL.Model.Avatar>().Update(large);
                Unit.Repository<DAL.Model.Avatar>().Update(medium);

                await Unit.SaveAsync();
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

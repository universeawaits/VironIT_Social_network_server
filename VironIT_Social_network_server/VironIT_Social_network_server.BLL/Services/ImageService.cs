using AutoMapper;
using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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

        public void Add(ImageDTO entity)
        {
            throw new NotImplementedException();
        }

        public void AddAll(IEnumerable<ImageDTO> entitis)
        {
            throw new NotImplementedException();
        }

        public async Task<ImageDTO> CompressAsync(ImageDTO image, uint width, uint height)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ImageDTO>> GetAllAsunc()
        {
            throw new NotImplementedException();
        }

        public async Task<ImageDTO> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Stream> ReadAsync(ImageDTO image)
        {
            throw new NotImplementedException();
        }

        public async void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ImageDTO> ResizeAsync(ImageDTO image, uint width, uint height)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(ImageDTO entity)
        {
            throw new NotImplementedException();
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

            var largeLink = "https://localhost:44334/images/avatars/large/" + uniqueFileName;
            var mediumLink = "https://localhost:44334/images/avatars/medium/" + uniqueFileName;

            using (var largeFileStream = new FileStream(Path.Combine(largeAvatarPath, uniqueFileName), FileMode.Create))
            {
                await image.CopyToAsync(largeFileStream);

                ImageDTO newAvatar = new ImageDTO
                {
                    Link = largeLink,
                    UserEmail = userEmail
                };
                await Unit.Repository<DAL.Model.Image>().CreateAsync(mapper.Map<ImageDTO, DAL.Model.Image>(newAvatar));
            }
            Resize(Path.Combine(largeAvatarPath, uniqueFileName), largeSize);

            image.Position = 0;
            using (var mediumFileStream = new FileStream(Path.Combine(mediumAvatarPath, uniqueFileName), FileMode.Create))
            {
                await image.CopyToAsync(mediumFileStream);

                ImageDTO newContactImage = new ImageDTO
                {
                    Link = mediumLink,
                    UserEmail = userEmail
                };
                await Unit.Repository<DAL.Model.Image>().CreateAsync(mapper.Map<ImageDTO, DAL.Model.Image>(newContactImage));
            }
            Resize(Path.Combine(mediumAvatarPath, uniqueFileName), mediumSize);
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

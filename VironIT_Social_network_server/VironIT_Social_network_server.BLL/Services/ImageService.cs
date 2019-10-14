using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.BLL.Services.Interface;

namespace VironIT_Social_network_server.BLL.Services
{
    public class ImageService : IImageService
    {
        public Task AddAsync(EntityDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(IEnumerable<EntityDTO> entities)
        {
            throw new NotImplementedException();
        }

        public Task<ImageDTO> Compress(int id, uint width, uint height)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(IEnumerable<int> entitiesIds)
        {
            throw new NotImplementedException();
        }

        public Task<ImageDTO> Resize(int id, uint width, uint height)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(EntityDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IEnumerable<EntityDTO> entities)
        {
            throw new NotImplementedException();
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

using VironIT_Social_network_server.BLL.DTO;


namespace VironIT_Social_network_server.BLL.Services.Interface
{
    public interface IService<TEntity> where TEntity : EntityDTO
    {
        Task<TEntity> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsunc();
        void Add(TEntity entity);
        void AddAll(IEnumerable<TEntity> entitis);
        void Remove(int id);
        Task UpdateAsync(TEntity entity);
    }
}

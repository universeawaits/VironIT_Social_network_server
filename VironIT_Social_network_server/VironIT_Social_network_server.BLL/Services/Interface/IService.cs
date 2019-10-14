using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using VironIT_Social_network_server.BLL.DTO;
using VironIT_Social_network_server.DAL.UnitOfWork;

namespace VironIT_Social_network_server.BLL.Services.Interface
{
    public interface IService<TIUnitOfWork> where TIUnitOfWork : IUnitOfWork
    {
        Task AddAsync(EntityDTO entity);
        Task AddAsync(IEnumerable<EntityDTO> entities);
        Task RemoveAsync(int id);
        Task RemoveAsync(IEnumerable<int> entitiesIds);
        Task UpdateAsync(EntityDTO entity);
        Task UpdateAsync(IEnumerable<EntityDTO> entities);
    }
}

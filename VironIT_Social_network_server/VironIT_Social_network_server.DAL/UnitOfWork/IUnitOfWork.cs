using System;
using System.Threading.Tasks;

using VironIT_Social_network_server.DAL.Model;
using VironIT_Social_network_server.DAL.Repository;


namespace VironIT_Social_network_server.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        Task SaveAsync();
    }
}

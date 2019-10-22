using Microsoft.EntityFrameworkCore;

using System;
using System.Threading.Tasks;

using VironIT_Social_network_server.DAL.Model;
using VironIT_Social_network_server.DAL.Repository;


namespace VironIT_Social_network_server.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : Entity;

        Task SaveAsync();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}

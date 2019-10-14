using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using VironIT_Social_network_server.DAL.Model;


namespace VironIT_Social_network_server.DAL.Repository
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetById(int id, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetEntityByFilter(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task CreateAsync(TEntity item);
        void Update(TEntity item);
        Task DeleteAsync(int id);
    }
}

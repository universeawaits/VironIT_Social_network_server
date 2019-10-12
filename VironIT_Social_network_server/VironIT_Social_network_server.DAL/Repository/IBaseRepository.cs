using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using VironIT_Social_network_server.DAL.Model;


namespace VironIT_Social_network_server.DAL.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetList(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T> GetById(int id, params Expression<Func<T, object>>[] includes);
        Task<T> GetEntityByFilter(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task CreateAsync(T item);
        void Update(T item);
        Task DeleteAsync(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using VironIT_Social_network_server.DAL.Context;
using VironIT_Social_network_server.DAL.Model;


namespace VironIT_Social_network_server.DAL.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly ImageContext _db;

        public BaseRepository(ImageContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(T item)
        {
            await _db.Set<T>().AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _db.Set<T>().Remove(entity);
            }
        }

        public IQueryable<T> GetList(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _db.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            var entities = query.Where(predicate);

            return entities;
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _db.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            var entities = await query.ToListAsync();

            return entities;
        }

        public void Update(T item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public async Task<T> GetById(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _db.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            return await query.SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> GetEntityByFilter(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _db.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            return await query.SingleOrDefaultAsync(predicate);
        }
    }
}

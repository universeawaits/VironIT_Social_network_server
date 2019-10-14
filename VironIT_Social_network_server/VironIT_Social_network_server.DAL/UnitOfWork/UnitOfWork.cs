using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using VironIT_Social_network_server.DAL.Model;
using VironIT_Social_network_server.DAL.Repository;


namespace VironIT_Social_network_server.DAL.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        public TContext Context { get; }
        private Dictionary<string, object> repositories;

        public UnitOfWork(TContext context)
        {
            Context = context;
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : Entity
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            var type = typeof(TEntity).Name;

            if (!repositories.ContainsKey(type))
            {
                repositories.Add(type, new Repository<TEntity, TContext>(Context));
            }

            return (Repository<TEntity, TContext>)repositories[type];
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}


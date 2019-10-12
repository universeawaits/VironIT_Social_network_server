using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using VironIT_Social_network_server.DAL.Context;
using VironIT_Social_network_server.DAL.Model;
using VironIT_Social_network_server.DAL.Repository;


namespace VironIT_Social_network_server.DAL.UnitOfWork
{
    public class ImageUnit : IUnitOfWork
    {
        private bool disposed;

        private readonly ImageContext context;
        private Dictionary<string, object> repositories;

        public ImageUnit(ImageContext context)
        {
            this.context = context;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public IBaseRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            var type = typeof(TEntity).Name;

            if (!repositories.ContainsKey(type))
            {
                repositories.Add(type, new BaseRepository<TEntity>(context));
            }

            return (BaseRepository<TEntity>)repositories[type];
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
}

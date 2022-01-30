using System;
using Microsoft.EntityFrameworkCore;
using Poke.Core.Interfaces.Repositories;
using Poke.Infra.Context;

namespace Poke.Infra.Repositories
{
    public class EntityBaseRepository<TEntity> : IEntityBaseRepository<TEntity>
        where TEntity : class
    {
        protected readonly EntityContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        private bool _disposed = false;

        public EntityBaseRepository(EntityContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual void Add(TEntity obj)
        {
            _dbSet.Add(obj);
        }

        public virtual void Update(TEntity obj)
        {
            _dbSet.Update(obj);
        }

        public virtual void Remove(TEntity obj)
        {
            _dbSet.Remove(obj);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                    _disposed = true;
                }
            }
        }
    }
}

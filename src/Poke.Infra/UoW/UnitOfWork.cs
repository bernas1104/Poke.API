using System;
using Microsoft.EntityFrameworkCore.Storage;
using Poke.Core.Interfaces.UoW;
using Poke.Infra.Context;

namespace Poke.Infra.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EntityContext _entityContext;
        private IDbContextTransaction _transaction;
        private bool _disposed = false;

        public UnitOfWork(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public int Commit()
        {
            return _entityContext.SaveChanges();
        }

        public void BeginTransaction()
        {
            _transaction = _entityContext.Database.BeginTransaction();
        }

        public void BeginCommit()
        {
            _transaction.Commit();
        }

        public void BeginRollback()
        {
            _transaction.Rollback();
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
                    _entityContext.Dispose();

                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }

                    _disposed = true;
                }
            }
        }
    }
}

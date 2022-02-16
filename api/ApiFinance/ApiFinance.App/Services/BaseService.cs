using ApiFinance.App.Contracts.Services;
using ApiFinance.Data.Contracts;
using System;

namespace ApiFinance.App.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity>
    {
        private bool _disposed;
        readonly IBaseRepository<TEntity> _iBaseRepository;

        public BaseService(IBaseRepository<TEntity> iBaseRepository) => _iBaseRepository = iBaseRepository;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
                _iBaseRepository?.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
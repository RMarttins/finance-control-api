using ApiFinance.Data.Contracts;
using Microsoft.Extensions.Configuration;
using System;

namespace ApiFinance.Data.Repositories
{
    public class BaseRepositoy<TEntity> : IBaseRepository<TEntity>
    {
        private bool _disposed = false;
        protected IDataContext DataContext;

        public BaseRepositoy(IDataContext dataContext, IConfiguration configuration)
        {
            DataContext = dataContext;
            DbSchema = configuration["DataConnection:Schema"];
            ParamSymbol = configuration["DataConnection:ParamSymbol"];
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
                DataContext?.Dispose();
            
            _disposed = true;
        }

        protected string DbSchema { get; private set; }
        protected string ParamSymbol { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
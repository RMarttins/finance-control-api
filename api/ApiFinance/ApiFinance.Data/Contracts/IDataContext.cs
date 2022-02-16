using System;
using System.Data;

namespace ApiFinance.Data.Contracts
{
    public interface IDataContext : IDisposable
    {
        void ClearTransaction();
        void CloseConnection();
        void OpenConnection();
        void SetTransacion(IDbTransaction dbTransaction);
        IDbConnection DataConnection { get; }
        IDbTransaction DbTransaction { get; }
    }
}
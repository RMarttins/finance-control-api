using ApiFinance.Data.Contracts;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace ApiFinance.Data.Context
{
    public class MySqlDataContext : IDataContext
    {
        private bool _disposed;
        private readonly MySqlConnection _mySqlConnection;

        public MySqlDataContext(IConfiguration configuration)
        {
            var connectionString = configuration["Connection:ConnectionString"];
            _mySqlConnection = new MySqlConnection(connectionString);
        }

        public IDbConnection DataConnection => _mySqlConnection;

        public IDbTransaction DbTransaction { get; private set; }

        public void ClearTransaction() => DbTransaction = null;

        public void CloseConnection()
        {
            if (_mySqlConnection != null && _mySqlConnection.State == ConnectionState.Open)
                _mySqlConnection.Close();
        }

        public void OpenConnection()
        {
            if (_mySqlConnection != null && _mySqlConnection.State == ConnectionState.Closed)
                _mySqlConnection.Open();
        }

        public void SetTransacion(IDbTransaction dbTransaction)
        {
            if(DbTransaction == null)
                DbTransaction = dbTransaction;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _mySqlConnection?.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
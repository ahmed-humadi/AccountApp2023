using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
namespace DataAccessLib
{
    public class DataAccess : IDisposable
    {
        private readonly object lockToke = new object();
        private string _connectionString = null;
        private SqlConnection _connection = null;
        private SqlCommand _command = null;
        private SqlTransaction _transaction = null;
        private SqlDataReader _sqlDataReader = null;
        private bool disposedValue = false;
        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new SqlConnection(_connectionString);
        }
        #region Methods
        public void OpenSqlConnection()
        {
            lock (lockToke)
            {
                if (!(_connection is null) && _connection.State == ConnectionState.Closed)
                    _connection.Open();
            }
        }
        public void CloseSqlConnection()
        {
            lock (lockToke)
            {
                if (!(_connection is null) && _connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }
        public void ExecuteQueryCommand(string commandString, CommandType commandType)
        {
            lock (lockToke)
            {
                if (!(_connection is null))
                {

                    _command = _connection.CreateCommand();
                    _command.CommandType = commandType;
                    _command.CommandText = commandString;
                    _command.Transaction = _transaction;
                    _command.Prepare();

                }
            }
        }
        public void ExecuteNonQueryCommand(string commandString, CommandType commandType, SqlParameter[] sqlParameters)
        {
            lock (lockToke)
            {
                if (!(_connection is null))
                {
                    _command = _connection.CreateCommand();
                    _command.CommandType = commandType;
                    _command.CommandText = commandString;
                    _command.Transaction = _transaction;
                    _command.Parameters.AddRange(sqlParameters);
                    _command.Prepare();
                }
            }
        }
        public void ExecuteNonQuery()
        {
            lock (lockToke)
            {
                if (!(_connection is null))
                {

                    _command.ExecuteNonQuery();

                }
            }
        }
        public IDataReader DataReader()
        {
            lock (lockToke)
            {
                if (!(_command is null))
                {

                    _sqlDataReader = _command.ExecuteReader();

                }
                return _sqlDataReader;
            }
        }
        public void CloseDataReader ()
        {
            lock (lockToke)
            {
                if (!_sqlDataReader.IsClosed)
                    _sqlDataReader.Close();
            }
        }
        public void BeginSqlTransaction()
        {
            lock (lockToke)
            {
                if (_transaction is null)
                    _transaction = _connection.BeginTransaction();
            }
        }
        public void CommitAction()
        {
            lock (lockToke)
            {
                if (!(_transaction is null))
                    _transaction.Commit();
            }
        }
        public void RollBackAction()
        {
            lock (lockToke)
            {
                if (!(_transaction is null))
                    _transaction.Rollback();
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    if (!(_connection is null))
                        _connection.Dispose();
                    if (!(_command is null))
                        _command.Dispose();
                    if (!(_transaction is null))
                        _transaction.Dispose();
                }
                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _connection = null;
                _command = null;
                _transaction = null;
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

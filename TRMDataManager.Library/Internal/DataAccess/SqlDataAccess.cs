using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMDataManager.Library.Internal.DataAccess
{
    internal class SqlDataAccess : IDisposable
    {
        private readonly IConfiguration _config;
        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }
        public string GetConnectionString(string name)
        {
            return _config.GetConnectionString(name);
        }

        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure).ToList();

                return rows;
            }
        }

        public void SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        //Open Connection/Start Transaction Method
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private readonly IConfiguration config;

        public void StartTransaction(string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            _connection = new SqlConnection(connectionString);
            _connection.Open();

            _transaction = _connection.BeginTransaction();
        }

        //Close connection /stop tansaction method
        public void CommitTransaction()
        {
            _transaction?.Commit();
            _connection?.Close();
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _connection?.Close();
        }

        //Dispose
        public void Dispose()
        {
            CommitTransaction();
        }

        //Save using the transaction
        public void SaveDataInTransaction<T>(string storedProcedure, T parameters)
        {
                _connection.Execute(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        //Load using the transaction 
        public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters)
        {
                List<T> rows = _connection.Query<T>(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();

                return rows;
        }
    }
}

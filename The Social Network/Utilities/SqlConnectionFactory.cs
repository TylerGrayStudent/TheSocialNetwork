using System;
using System.Data;
using System.Data.SqlClient;
using The_Social_Network.Utilities.Interfaces;

namespace The_Social_Network.Utilities
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _dbConnectionString;

        public SqlConnectionFactory()
        {
            _dbConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        }
        
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_dbConnectionString);
        }
    }
}
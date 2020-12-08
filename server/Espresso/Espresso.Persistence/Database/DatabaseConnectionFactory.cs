using System.Data;
using Microsoft.Data.SqlClient;

namespace Espresso.Persistence.Database
{
    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        #region Fields
        private readonly string _connectionString;
        #endregion

        #region Constructors
        public DatabaseConnectionFactory(
            string connectionString
        )
        {
            _connectionString = connectionString;
        }
        #endregion

        #region Methods
        public IDbConnection CreateDatabaseConnection()
        {
            return new SqlConnection(_connectionString);
        }
        #endregion
    }
}

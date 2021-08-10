// DatabaseConnectionFactory.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Data;
using Npgsql;

namespace Espresso.Persistence.Database
{
    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConnectionFactory"/> class.
        /// </summary>
        /// <param name="connectionString"></param>
        public DatabaseConnectionFactory(
            string connectionString
        )
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateDatabaseConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}

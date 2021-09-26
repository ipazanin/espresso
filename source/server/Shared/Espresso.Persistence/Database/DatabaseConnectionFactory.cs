// DatabaseConnectionFactory.cs
//
// © 2021 Espresso News. All rights reserved.

using Npgsql;
using System.Data;

namespace Espresso.Persistence.Database
{
    /// <summary>
    /// Dataase connection factory.
    /// </summary>
    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConnectionFactory"/> class.
        /// </summary>
        /// <param name="connectionString">Database connection string.</param>
        public DatabaseConnectionFactory(
            string connectionString
        )
        {
            _connectionString = connectionString;
        }

        /// <inheritdoc/>
        public IDbConnection CreateDatabaseConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}

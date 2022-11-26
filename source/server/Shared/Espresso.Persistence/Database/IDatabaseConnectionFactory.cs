// IDatabaseConnectionFactory.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Data;

namespace Espresso.Persistence.Database;

/// <summary>
/// Database connetion factory.
/// </summary>
public interface IDatabaseConnectionFactory
{
    /// <summary>
    /// Creates database connection.
    /// </summary>
    /// <returns>Database connection.</returns>
    public IDbConnection CreateDatabaseConnection();
}

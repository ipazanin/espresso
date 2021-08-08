// IDatabaseConnectionFactory.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Data;

namespace Espresso.Persistence.Database
{
    public interface IDatabaseConnectionFactory
    {
        public IDbConnection CreateDatabaseConnection();
    }
}

using System.Data;

namespace Espresso.Persistence.Database
{
    public interface IDatabaseConnectionFactory
    {
        public IDbConnection CreateDatabaseConnection();
    }
}

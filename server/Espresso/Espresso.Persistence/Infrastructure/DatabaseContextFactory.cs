using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Persistence.Infrastructure
{
    public class DatabaseContextFactory : DesignTimeDatabaseContextFactoryBase<ApplicationDatabaseContext>
    {
        protected override ApplicationDatabaseContext CreateNewInstance(DbContextOptions<ApplicationDatabaseContext> options)
        {
            return new ApplicationDatabaseContext(options);
        }
    }
}

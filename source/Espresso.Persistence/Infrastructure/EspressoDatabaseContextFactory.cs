using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Persistence.Infrastructure
{
    public class EspressoDatabaseContextFactory : DesignTimeDatabaseContextFactoryBase<EspressoDatabaseContext>
    {
        public EspressoDatabaseContextFactory()
            : base() { }

        protected override EspressoDatabaseContext CreateNewInstance(DbContextOptions<EspressoDatabaseContext> options)
        {
            return new EspressoDatabaseContext(options);
        }
    }
}

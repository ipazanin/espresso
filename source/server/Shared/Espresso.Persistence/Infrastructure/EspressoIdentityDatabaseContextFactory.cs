// EspressoIdentityDatabaseContextFactory.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Persistence.Infrastructure
{
    public class EspressoIdentityDatabaseContextFactory : DesignTimeDatabaseContextFactoryBase<EspressoIdentityDatabaseContext>
    {
        protected override EspressoIdentityDatabaseContext CreateNewInstance(DbContextOptions<EspressoIdentityDatabaseContext> options)
        {
            return new EspressoIdentityDatabaseContext(options);
        }
    }
}

// EspressoIdentityDatabaseContextFactory.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Persistence.Infrastructure
{
    /// <inheritdoc/>
    public class EspressoIdentityDatabaseContextFactory : DesignTimeDatabaseContextFactoryBase<EspressoIdentityDatabaseContext>
    {
        /// <inheritdoc/>
        protected override EspressoIdentityDatabaseContext CreateNewInstance(DbContextOptions<EspressoIdentityDatabaseContext> options)
        {
            return new EspressoIdentityDatabaseContext(options);
        }
    }
}

// DatabaseContextFactory.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Persistence.Infrastructure;

/// <inheritdoc/>
public class DatabaseContextFactory : DesignTimeDatabaseContextFactoryBase<EspressoDatabaseContext>
{
    /// <inheritdoc/>
    protected override EspressoDatabaseContext CreateNewInstance(DbContextOptions<EspressoDatabaseContext> options)
    {
        return new EspressoDatabaseContext(options);
    }
}

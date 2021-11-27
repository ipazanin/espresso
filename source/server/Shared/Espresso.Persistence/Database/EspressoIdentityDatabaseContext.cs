// EspressoIdentityDatabaseContext.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Persistence.Database;

/// <summary>
/// Espresso identity database.
/// </summary>
public class EspressoIdentityDatabaseContext : IdentityDbContext, IEspressoIdentityDatabaseContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EspressoIdentityDatabaseContext"/> class.
    /// </summary>
    /// <param name="options">Database context options.</param>
    public EspressoIdentityDatabaseContext(DbContextOptions<EspressoIdentityDatabaseContext> options)
        : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}

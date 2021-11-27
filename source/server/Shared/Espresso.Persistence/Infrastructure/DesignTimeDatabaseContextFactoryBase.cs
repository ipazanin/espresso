// DesignTimeDatabaseContextFactoryBase.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Espresso.Persistence.Infrastructure;

/// <summary>
/// Design time database context factory base.
/// </summary>
/// <typeparam name="TContext"><see cref="DbContext"/> class.</typeparam>
public abstract class DesignTimeDatabaseContextFactoryBase<TContext> :
    IDesignTimeDbContextFactory<TContext>
    where TContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DesignTimeDatabaseContextFactoryBase{TContext}"/> class.
    /// </summary>
    protected DesignTimeDatabaseContextFactoryBase()
    {
    }

    /// <summary>
    /// Creates new instance of <typeparamref name="TContext"/>.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    /// <returns>Database context.</returns>
    public TContext CreateDbContext(string[] args)
    {
        var connectionString = args.Length != 1 ?
            throw new Exception("Connection String must be specified as first argument!") :
            args[0];

        return Create(connectionString);
    }

    /// <summary>
    /// Creates new instance of <typeparamref name="TContext"/>.
    /// </summary>
    /// <param name="options">Database context options.</param>
    /// <returns>Database context.</returns>
    protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

    private TContext Create(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("Connection string is null or empty.", nameof(connectionString));
        }

        Console.WriteLine($"{nameof(DesignTimeDatabaseContextFactoryBase<TContext>)}.{nameof(Create)}(string): Connection string: '{connectionString}'.");

        var optionsBuilder = new DbContextOptionsBuilder<TContext>();

        optionsBuilder.EnableSensitiveDataLogging();

        optionsBuilder.UseNpgsql(connectionString);

        return CreateNewInstance(optionsBuilder.Options);
    }
}

// IEspressoIdentityDatabaseContext.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Espresso.Persistence.Database;

/// <summary>
/// Espresso identity database context.
/// </summary>
public interface IEspressoIdentityDatabaseContext
{
    /// <summary>
    /// Gets object that provides access to database related information and operations for this context.
    /// </summary>
    public DatabaseFacade Database { get; }

    /// <summary>
    ///  Creates a Microsoft.EntityFrameworkCore.DbSet`1 that can be used to query and save instances of TEntity.
    /// </summary>
    /// <typeparam name="T">The type of entity for which a set should be returned.</typeparam>
    /// <returns>A set for the given entity type.</returns>
    public DbSet<T> Set<T>()
        where T : class;

    /// <summary>
    /// Saves all changes made in this context to the database. <br/>
    /// This method will automatically call Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges
    /// to discover any changes to entity instances before saving to the underlying database. <br/>
    /// This can be disabled via Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled. <br/>
    /// Multiple active operations on the same context instance are not supported. Use
    /// 'await' to ensure that any asynchronous operations have completed before calling
    /// another method on this context.
    /// </summary>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <exception cref="DbUpdateConcurrencyException">
    /// A concurrency violation is encountered while saving to the database. A concurrency violation occurs when an unexpected number of rows are affected during save.
    /// This is usually because the data in the database has been modified since it was loaded into memory.
    /// </exception>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

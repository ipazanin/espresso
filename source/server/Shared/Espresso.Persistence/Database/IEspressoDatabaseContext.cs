// IEspressoDatabaseContext.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Espresso.Persistence.Database;

/// <summary>
/// Espresso database context.
/// </summary>
public interface IEspressoDatabaseContext
{
    /// <summary>
    /// Gets <see cref="NewsPortal"/> database set.
    /// </summary>
    public DbSet<NewsPortal> NewsPortals { get; }

    /// <summary>
    /// Gets <see cref="RssFeed"/> database set.
    /// </summary>
    public DbSet<RssFeed> RssFeeds { get; }

    /// <summary>
    /// Gets <see cref="Article"/> database set.
    /// </summary>
    public DbSet<Article> Articles { get; }

    /// <summary>
    /// Gets <see cref="Category"/> database set.
    /// </summary>
    public DbSet<Category> Categories { get; }

    /// <summary>
    /// Gets <see cref="ArticleCategory"/> database set.
    /// </summary>
    public DbSet<ArticleCategory> ArticleCategories { get; }

    /// <summary>
    /// Gets <see cref="Domain.Entities.ApplicationDownload"/> database set.
    /// </summary>
    public DbSet<ApplicationDownload> ApplicationDownload { get; }

    /// <summary>
    /// Gets <see cref="Domain.Entities.RssFeedCategory"/> database set.
    /// </summary>
    public DbSet<RssFeedCategory> RssFeedCategory { get; }

    /// <summary>
    /// Gets <see cref="PushNotification"/> database set.
    /// </summary>
    public DbSet<PushNotification> PushNotifications { get; }

    /// <summary>
    /// Gets <see cref="Region"/> database set.
    /// </summary>
    public DbSet<Region> Regions { get; }

    /// <summary>
    /// Gets <see cref="SimilarArticle"/> database set.
    /// </summary>
    public DbSet<SimilarArticle> SimilarArticles { get; }

    /// <summary>
    /// Gets <see cref="Setting"/> database set.
    /// </summary>
    public DbSet<Setting> Settings { get; }

    public DbSet<RssFeedContentModifier> RssFeedContentModifiers { get; }

    public DbSet<NewsPortalImage> NewsPortalImages { get; }

    /// <summary>
    /// Gets object that provides access to database related information and operations for this context.
    /// </summary>
    public DatabaseFacade Database { get; }

    public ChangeTracker ChangeTracker { get; }

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

    /// <summary>
    /// Gets an Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry`1 for the given entity. The entry provides access to change tracking information and operations for the entity.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="entity">The entity to get the entry for.</param>
    /// <returns>The entry for the given entity.</returns>
    public EntityEntry<T> Entry<T>(T entity)
        where T : class;
}

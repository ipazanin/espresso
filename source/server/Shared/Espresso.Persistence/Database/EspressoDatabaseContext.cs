// EspressoDatabaseContext.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Persistence.Database;

/// <summary>
/// Espresso database context.
/// </summary>
public class EspressoDatabaseContext : DbContext, IEspressoDatabaseContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EspressoDatabaseContext"/> class.
    /// </summary>
    /// <param name="options">Database context options.</param>
    public EspressoDatabaseContext(DbContextOptions<EspressoDatabaseContext> options)
            : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    /// <summary>
    /// Gets or sets <see cref="NewsPortal"/> database set.
    /// </summary>
    public DbSet<NewsPortal> NewsPortals { get; init; } = null!;

    /// <summary>
    /// Gets or sets <see cref="RssFeed"/> database set.
    /// </summary>
    public DbSet<RssFeed> RssFeeds { get; init; } = null!;

    /// <summary>
    /// Gets or sets <see cref="Article"/> database set.
    /// </summary>
    public DbSet<Article> Articles { get; init; } = null!;

    /// <summary>
    /// Gets or sets <see cref="Category"/> database set.
    /// </summary>
    public DbSet<Category> Categories { get; init; } = null!;

    /// <summary>
    /// Gets or sets <see cref="ArticleCategory"/> database set.
    /// </summary>
    public DbSet<ArticleCategory> ArticleCategories { get; init; } = null!;

    /// <summary>
    /// Gets or sets <see cref="Domain.Entities.ApplicationDownload"/> database set.
    /// </summary>
    public DbSet<ApplicationDownload> ApplicationDownload { get; init; } = null!;

    /// <summary>
    /// Gets or sets <see cref="Domain.Entities.RssFeedCategory"/> database set.
    /// </summary>
    public DbSet<RssFeedCategory> RssFeedCategory { get; init; } = null!;

    /// <summary>
    /// Gets or sets <see cref="PushNotification"/> database set.
    /// </summary>
    public DbSet<PushNotification> PushNotifications { get; init; } = null!;

    /// <summary>
    /// Gets or sets <see cref="Region"/> database set.
    /// </summary>
    public DbSet<Region> Regions { get; init; } = null!;

    /// <summary>
    /// Gets or sets <see cref="SimilarArticle"/> database set.
    /// </summary>
    public DbSet<SimilarArticle> SimilarArticles { get; init; } = null!;

    /// <summary>
    /// Gets or sets <see cref="Setting"/> database set.
    /// </summary>
    public DbSet<Setting> Settings { get; init; } = null!;

    public DbSet<NewsPortalImage> NewsPortalImages { get; init; } = null!;

    public DbSet<RssFeedContentModifier> RssFeedContentModifiers { get; init; } = null!;

    public DbSet<Country> Countries { get; init; } = null!;

    public DbSet<CountryImage> CountryImages { get; init; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyAllConfigurations(
            configurationsAssembly: typeof(EspressoDatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

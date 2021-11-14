// EspressoDatabaseContext.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Persistence.Database
{
    /// <summary>
    /// Espresso database context.
    /// </summary>
    public class EspressoDatabaseContext : DbContext, IEspressoDatabaseContext
    {
        /// <summary>
        /// Gets or sets <see cref="NewsPortal"/> database set.
        /// </summary>
        public DbSet<NewsPortal> NewsPortals { get; set; } = null!;

        /// <summary>
        /// Gets or sets <see cref="RssFeed"/> database set.
        /// </summary>
        public DbSet<RssFeed> RssFeeds { get; set; } = null!;

        /// <summary>
        /// Gets or sets <see cref="Article"/> database set.
        /// </summary>
        public DbSet<Article> Articles { get; set; } = null!;

        /// <summary>
        /// Gets or sets <see cref="Category"/> database set.
        /// </summary>
        public DbSet<Category> Categories { get; set; } = null!;

        /// <summary>
        /// Gets or sets <see cref="ArticleCategory"/> database set.
        /// </summary>
        public DbSet<ArticleCategory> ArticleCategories { get; set; } = null!;

        /// <summary>
        /// Gets or sets <see cref="Espresso.Domain.Entities.ApplicationDownload"/> database set.
        /// </summary>
        public DbSet<ApplicationDownload> ApplicationDownload { get; set; } = null!;

        /// <summary>
        /// Gets or sets <see cref="Espresso.Domain.Entities.RssFeedCategory"/> database set.
        /// </summary>
        public DbSet<RssFeedCategory> RssFeedCategory { get; set; } = null!;

        /// <summary>
        /// Gets or sets <see cref="PushNotification"/> database set.
        /// </summary>
        public DbSet<PushNotification> PushNotifications { get; set; } = null!;

        /// <summary>
        /// Gets or sets <see cref="Region"/> database set.
        /// </summary>
        public DbSet<Region> Regions { get; set; } = null!;

        /// <summary>
        /// Gets or sets <see cref="SimilarArticle"/> database set.
        /// </summary>
        public DbSet<SimilarArticle> SimilarArticles { get; set; } = null!;

        /// <summary>
        /// Gets or sets <see cref="Setting"/> database set.
        /// </summary>
        public DbSet<Setting> Settings { get; set; } = null!;

        /// <summary>
        /// Initializes a new instance of the <see cref="EspressoDatabaseContext"/> class.
        /// </summary>
        /// <param name="options">Database context options.</param>
#pragma warning disable SA1201 // Elements should appear in the correct order
        public EspressoDatabaseContext(DbContextOptions<EspressoDatabaseContext> options)
#pragma warning restore SA1201 // Elements should appear in the correct order
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations(
                configurationsAssembly: typeof(EspressoDatabaseContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

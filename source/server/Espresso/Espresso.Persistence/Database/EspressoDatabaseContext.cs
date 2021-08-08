// EspressoDatabaseContext.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Persistence.Database
{
    public class EspressoDatabaseContext : DbContext, IEspressoDatabaseContext
    {
        public DbSet<NewsPortal> NewsPortals { get; set; } = null!;

        public DbSet<RssFeed> RssFeeds { get; set; } = null!;

        public DbSet<Article> Articles { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<ArticleCategory> ArticleCategories { get; set; } = null!;

        public DbSet<ApplicationDownload> ApplicationDownload { get; set; } = null!;

        public DbSet<RssFeedCategory> RssFeedCategory { get; set; } = null!;

        public DbSet<PushNotification> PushNotifications { get; set; } = null!;

        public DbSet<Region> Regions { get; set; } = null!;

        public DbSet<SimilarArticle> SimilarArticles { get; set; } = null!;

        /// <summary>
        /// Initializes a new instance of the <see cref="EspressoDatabaseContext"/> class.
        /// </summary>
        /// <param name="options"></param>
        public EspressoDatabaseContext(DbContextOptions<EspressoDatabaseContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations(
                configurationsAssembly: typeof(EspressoDatabaseContext).Assembly
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}

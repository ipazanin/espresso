using System;
using Espresso.Domain.Entities;
using Espresso.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Persistence.Database
{
    public class EspressoDatabaseContext : DbContext, IEspressoDatabaseContext
    {
        #region Properties
        public DbSet<NewsPortal> NewsPortals { get; set; } = null!;
        public DbSet<RssFeed> RssFeeds { get; set; } = null!;
        public DbSet<Article> Articles { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<ArticleCategory> ArticleCategories { get; set; } = null!;
        public DbSet<ApplicationDownload> ApplicationDownload { get; set; } = null!;
        public DbSet<RssFeedCategory> RssFeedCategory { get; set; } = null!;
        #endregion

        #region Constructors
        public EspressoDatabaseContext(DbContextOptions<EspressoDatabaseContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            //ChangeTracker.AutoDetectChangesEnabled = false;
            Database.SetCommandTimeout(TimeSpan.FromMinutes(2));
        }
        #endregion

        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyAllConfigurations();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        #endregion
    }
}

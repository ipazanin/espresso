using Espresso.Domain.Entities;
using Espresso.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Persistence.Database
{
    public class ApplicationDatabaseContext : DbContext, IApplicationDatabaseContext
    {
        #region Properties
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
        #endregion

        #region Constructors
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }
        #endregion

        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations();
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        #endregion
    }
}

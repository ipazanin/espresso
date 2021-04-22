using System.Threading;
using System.Threading.Tasks;
using Espresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Espresso.Persistence.Database
{
    public interface IEspressoDatabaseContext
    {
        #region Properties

        public DbSet<NewsPortal> NewsPortals { get; }
        public DbSet<RssFeed> RssFeeds { get; }
        public DbSet<Article> Articles { get; }
        public DbSet<Category> Categories { get; }
        public DbSet<ArticleCategory> ArticleCategories { get; }
        public DbSet<ApplicationDownload> ApplicationDownload { get; }
        public DbSet<RssFeedCategory> RssFeedCategory { get; }
        public DbSet<PushNotification> PushNotifications { get; }
        public DbSet<Region> Regions { get; }
        public DbSet<SimilarArticle> SimilarArticles { get; }

        public DatabaseFacade Database { get; }

        #endregion

        #region Methods

        public DbSet<T> Set<T>() where T : class;

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        public EntityEntry<T> Entry<T>(T entity) where T : class;
        #endregion
    }
}

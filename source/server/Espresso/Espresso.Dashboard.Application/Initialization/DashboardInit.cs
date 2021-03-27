using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Domain.Entities;
using Espresso.Common.Extensions;
using Espresso.Domain.IServices;
using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace Espresso.Dashboard.Application.Initialization
{
    /// <summary>
    /// 
    /// </summary>
    public class DashboardInit : IDashboardInit
    {
        #region Fields

        private readonly IMemoryCache _memoryCache;
        private readonly IEspressoDatabaseContext _context;
        private readonly IEspressoIdentityDatabaseContext _espressoIdentityContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly string _adminUserPassword;
        private readonly ILoggerService<DashboardInit> _loggerService;

        private readonly IEnumerable<string> _adminUserEmails = new[]
        {
            "ivan.pazanin1996@gmail.com",
            "miro@espressonews.co",
            "nikola.dadic@gmail.com"
        };

        #endregion
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="memoryCache"></param>
        /// <param name="context"></param>
        /// <param name="memoryCache"></param>
        /// <param name="context"></param>
        /// <param name="memoryCache"></param>
        /// <param name="loggerFactory"></param>
        public DashboardInit(
            IMemoryCache memoryCache,
            IEspressoDatabaseContext context,
            IEspressoIdentityDatabaseContext espressoIdentityContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            string adminUserPassword,
            ILoggerService<DashboardInit> loggerService
        )
        {
            _memoryCache = memoryCache;
            _context = context;
            _espressoIdentityContext = espressoIdentityContext;
            _roleManager = roleManager;
            _userManager = userManager;
            _adminUserPassword = adminUserPassword;
            _loggerService = loggerService;
        }
        #endregion

        #region Methods
        public async Task InitParserDeleter()
        {
            await InitEspressoDatabaseAndMemoryCache();
            await InitEspressoIdentityDatabase();
        }

        private async Task InitEspressoDatabaseAndMemoryCache()
        {
            var isInitialized = _memoryCache.Get<IEnumerable<NewsPortal>?>(key: MemoryCacheConstants.NewsPortalKey) != null;
            if (isInitialized)
            {
                return;
            }

            var stopwatch = Stopwatch.StartNew();

            await _context.Database.MigrateAsync();

            #region NewsPortals
            var newsPortals = await _context
                .NewsPortals
                .AsNoTracking()
                .ToListAsync();

            var newsPortalsDictionary = newsPortals.ToDictionary(newsPortal => newsPortal.Id);

            _memoryCache.Set(
                key: MemoryCacheConstants.NewsPortalKey,
                value: newsPortals
            );
            #endregion

            #region Categories
            var categories = await _context
                .Categories
                .AsNoTracking()
                .ToListAsync();

            var categoriesDictionary = categories.ToDictionary(category => category.Id);

            _memoryCache.Set(
                key: MemoryCacheConstants.CategoryKey,
                value: categories
            );
            #endregion

            #region Articles
            var articles = await _context.Articles
                .Include(article => article.ArticleCategories)
                .Include(article => article.MainArticle)
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();

            var articlesDictionary = articles.ToDictionary(article => article.Id);

            foreach (var article in articles)
            {
                var newsPortal = newsPortalsDictionary[article.NewsPortalId];
                article.SetNewsPortal(newsPortal);

                foreach (var articleCategory in article.ArticleCategories)
                {
                    var category = categoriesDictionary[articleCategory.CategoryId];
                    articleCategory.SetCategory(category);
                }
                if (article.MainArticle is not null)
                {
                    var mainArticle = articlesDictionary[article.MainArticle.MainArticleId];
                    mainArticle.SubordinateArticles.Add(article.MainArticle);
                    article.MainArticle.SetMainArticle(mainArticle);
                }
            }

            _memoryCache.Set(
                key: MemoryCacheConstants.ArticleKey,
                value: articlesDictionary
            );
            #endregion

            #region RssFeeds
            var rssFeeds = await _context.RssFeeds
                .Include(rssFeed => rssFeed.Category)
                .Include(rssFeed => rssFeed.NewsPortal)
                .Include(rssFeed => rssFeed.RssFeedCategories)
                .ThenInclude(rssFeedCategory => rssFeedCategory.Category)
                .Include(rssFeed => rssFeed.RssFeedContentModifiers)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();

            _memoryCache.Set(
                key: MemoryCacheConstants.RssFeedKey,
                value: rssFeeds.ToList()
            );
            #endregion
            stopwatch.Stop();

            var eventName = Event.DashboardEspressoDatabaseInit.GetDisplayName();
            var duration = stopwatch.Elapsed;
            var categoriesCount = categories.Count;
            var newsPortalsCount = newsPortals.Count;
            var allArticlesCount = articles.Count;
            var rssFeedCount = rssFeeds.Count;

            var arguments = new List<(string parameterName, object parameterValue)>
            {
                (nameof(duration), duration),
                (nameof(categoriesCount), categoriesCount),
                (nameof(newsPortalsCount), newsPortalsCount),
                (nameof(allArticlesCount), allArticlesCount),
                (nameof(rssFeedCount), rssFeedCount)
            };

            _loggerService.Log(eventName, LogLevel.Information, arguments);
        }

        private async Task InitEspressoIdentityDatabase()
        {
            await _espressoIdentityContext.Database.MigrateAsync();
            if (!await _roleManager.RoleExistsAsync(roleName: RoleConstants.AdminRoleName))
            {
                var adminRole = new IdentityRole(
                    roleName: RoleConstants.AdminRoleName
                );
                await _roleManager.CreateAsync(role: adminRole);
            }


            foreach (var adminUserEmail in _adminUserEmails)
            {
                if (!await _userManager.Users.AnyAsync(user => user.Email == adminUserEmail))
                {
                    var adminUser = new IdentityUser
                    {
                        Email = adminUserEmail,
                        NormalizedEmail = _userManager.NormalizeEmail(adminUserEmail),
                        UserName = adminUserEmail,
                        NormalizedUserName = _userManager.NormalizeEmail(adminUserEmail),
                        EmailConfirmed = true
                    };

                    adminUser.SecurityStamp = await _userManager.GetSecurityStampAsync(adminUser);

                    var identityResult = await _userManager.CreateAsync(
                        user: adminUser,
                        password: _adminUserPassword
                    );
                    await _userManager.AddToRoleAsync(adminUser, RoleConstants.AdminRoleName);
                }
            }
        }
        #endregion
    }
}

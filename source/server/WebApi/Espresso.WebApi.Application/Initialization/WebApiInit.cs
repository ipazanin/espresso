// WebApiInit.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Extensions;
using Espresso.Domain.IServices;
using Espresso.Persistence.Database;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Espresso.WebApi.Application.Initialization
{
    /// <summary>
    /// 
    /// </summary>
    public class WebApiInit : IWebApiInit
    {
        private const string ConfigurationFileName = "firebase-key.json";

        private readonly IMemoryCache _memoryCache;
        private readonly IEspressoDatabaseContext _context;
        private readonly ILoggerService<WebApiInit> _loggerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebApiInit"/> class.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="loggerService"></param>
        /// <param name="memoryCache"></param>
        /// <summary>
        /// Initializes a new instance of the <see cref="WebApiInit"/> class.
        /// </summary>
        /// <param name="memoryCache"></param>
        /// <param name="context"></param>
        /// <param name="loggerService"></param>
        public WebApiInit(
            IMemoryCache memoryCache,
            IEspressoDatabaseContext context,
            ILoggerService<WebApiInit> loggerService
        )
        {
            _memoryCache = memoryCache;
            _context = context;
            _loggerService = loggerService;
        }

        public async Task InitWebApi()
        {
            var stopwatch = Stopwatch.StartNew();

            InitializeGoogleServices();

            await _context.Database.MigrateAsync();

            var regions = await _context
                .Regions
                .Include(region => region.NewsPortals)
                .AsNoTracking()
                .ToListAsync();

            _memoryCache.Set(
                key: MemoryCacheConstants.RegionKey,
                value: regions.ToList()
            );

            var newsPortals = await _context
                .NewsPortals
                .Include(newsPortal => newsPortal.Category)
                .Include(newsPortal => newsPortal.Region)
                .AsNoTracking()
                .ToListAsync();

            var newsPortalsDictionary = newsPortals
                .ToDictionary(newsPortal => newsPortal.Id);

            _memoryCache.Set(
                key: MemoryCacheConstants.NewsPortalKey,
                value: newsPortals.ToList()
            );

            var categories = await _context
                .Categories
                .Include(category => category.NewsPortals)
                .AsNoTracking()
                .ToListAsync();

            var categoriesDictionary = categories.ToDictionary(category => category.Id);

            _memoryCache.Set(
                key: MemoryCacheConstants.CategoryKey,
                value: categories
            );

            var articles = await _context.Articles
                .Include(article => article.ArticleCategories)
                .Include(article => article.MainArticle)
                .AsSplitQuery()
                .AsNoTracking()
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
                value: articles
            );

            stopwatch.Stop();

            var eventName = Event.WebApiInit.GetDisplayName();
            var duration = stopwatch.Elapsed;
            var categoriesCount = categories.Count;
            var newsPortalsCount = newsPortals.Count;
            var allArticlesCount = articles.Count;

            var arguments = new (string, object)[]{
                (nameof(duration),duration),
                (nameof(categoriesCount),categoriesCount),
                (nameof(newsPortalsCount),newsPortalsCount),
                (nameof(allArticlesCount),allArticlesCount),
            };

            _loggerService.Log(eventName, LogLevel.Information, arguments);
        }

        private static void InitializeGoogleServices()
        {
            var firebaseKeyPath = Path.Combine(
                path1: AppDomain.CurrentDomain.BaseDirectory ?? string.Empty,
                path2: ConfigurationFileName
            );
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(firebaseKeyPath),
            });
        }
    }
}

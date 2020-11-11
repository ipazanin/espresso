using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Espresso.Application.IServices;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Utilities;
using Espresso.Domain.Entities;
using Espresso.Common.Extensions;
using Espresso.Domain.IServices;
using Espresso.Persistence.Database;
using Espresso.Persistence.IRepositories;
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
        #region Constants
        private const string ConfigurationFileName = "firebase-key.json";

        #endregion

        #region Fileds
        private readonly IMemoryCache _memoryCache;
        private readonly IApplicationDatabaseContext _context;
        private readonly ILoggerService<WebApiInit> _loggerService;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="memoryCache"></param>
        /// <param name="loggerFactory"></param>
        public WebApiInit(
            IMemoryCache memoryCache,
            IApplicationDatabaseContext context,
            ILoggerService<WebApiInit> loggerService
        )
        {
            _memoryCache = memoryCache;
            _context = context;
            _loggerService = loggerService;
        }
        #endregion

        #region Public Methods
        public async Task InitWebApi()
        {
            var stopwatch = Stopwatch.StartNew();

            InitializeFireBase();

            await _context.Database.MigrateAsync();


            #region Regions
            var regions = await _context
                .Regions
                .Include(region => region.NewsPortals)
                .AsNoTracking()
                .ToListAsync();

            _ = _memoryCache.Set(
                key: MemoryCacheConstants.RegionKey,
                value: regions.ToList()
            );
            #endregion

            #region NewsPortals
            var newsPortals = await _context
                .NewsPortals
                .Include(newsPortal => newsPortal.Category)
                .AsNoTracking()
                .ToListAsync();

            _ = _memoryCache.Set(
                key: MemoryCacheConstants.NewsPortalKey,
                value: newsPortals.ToList()
            );
            #endregion

            #region Categories
            var categories = await _context
                .Categories
                .Include(category => category.NewsPortals)
                .AsNoTracking()
                .ToListAsync();

            var categoryDictionary = categories.ToDictionary(category => category.Id);

            _ = _memoryCache.Set(
                key: MemoryCacheConstants.CategoryKey,
                value: categories.ToList()
            );
            #endregion

            #region Articles
            var articles = await _context.Articles
                .Include(article => article.ArticleCategories)
                .ThenInclude(articleCategory => articleCategory.Category)
                .Include(article => article.NewsPortal)
                .Include(article => article.MainArticle)
                .ThenInclude(mainArticle => mainArticle!.MainArticle)
                .Include(article => article.SubordinateArticles)
                .ThenInclude(subordinateArticle => subordinateArticle!.SubordinateArticle)
                .ThenInclude(article => article!.NewsPortal)
                .Include(article => article.SubordinateArticles)
                .ThenInclude(subordinateArticle => subordinateArticle!.SubordinateArticle)
                .ThenInclude(article => article!.ArticleCategories)
                .ThenInclude(articleCategory => articleCategory.Category)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();

            _ = _memoryCache.Set(
                key: MemoryCacheConstants.ArticleKey,
                value: articles
            );
            #endregion

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
        #endregion

        #region Private Methods
        private static void InitializeFireBase()
        {
            _ = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(
                path: Path.Combine(
                    path1: AppDomain.CurrentDomain.BaseDirectory ?? "",
                    path2: ConfigurationFileName
                )
            ),
            });
        }
        #endregion
    }
}

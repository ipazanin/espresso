using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Utilities;
using Espresso.Domain.Entities;
using Espresso.Domain.Extensions;
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
        private readonly ILogger<WebApiInit> _logger;
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
            ILoggerFactory loggerFactory
        )
        {
            _memoryCache = memoryCache;
            _context = context;
            _logger = loggerFactory.CreateLogger<WebApiInit>();
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
                .AsNoTracking()
                .ToListAsync();

            var newsPortalsDictionary = newsPortals.ToDictionary(newsPortal => newsPortal.Id);

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
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();

            _ = _memoryCache.Set(
                key: MemoryCacheConstants.ArticleKey,
                value: articles
            );
            #endregion

            stopwatch.Stop();

            var eventId = (int)Event.WebApiInit;
            var eventName = Event.WebApiInit.GetDisplayName();
            var duration = stopwatch.Elapsed;
            var categoriesCount = categories.Count;
            var newsPortalsCount = newsPortals.Count;
            var allArticlesCount = articles.Count;

            var message =
                $"{AnsiUtility.EncodeEventName($"{{@{nameof(eventName)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(duration))}: " +
                $"{AnsiUtility.EncodeTimespan($"{{@{nameof(duration)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(categoriesCount))}: " +
                $"{AnsiUtility.EncodeRequestParameters($"{{@{nameof(categoriesCount)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(newsPortalsCount))}: " +
                $"{AnsiUtility.EncodeRequestParameters($"{{@{nameof(newsPortalsCount)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(allArticlesCount))}: " +
                $"{AnsiUtility.EncodeRequestParameters($"{{@{nameof(allArticlesCount)}}}")}\n\t";

            var args = new object[]
            {
                eventName,
                duration,
                categoriesCount,
                newsPortalsCount,
                allArticlesCount,
            };

            _logger.LogInformation(
                eventId: new EventId(id: eventId, name: eventName),
                message: message,
                args: args
            );
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

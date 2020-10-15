using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Utilities;
using Espresso.Domain.Entities;
using Espresso.Domain.Extensions;
using Espresso.Persistence.Database;
using Espresso.Persistence.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Espresso.ParserDeleter.Application.Initialization
{
    /// <summary>
    /// 
    /// </summary>
    public class ParserDeleterInit : IParserDeleterInit
    {
        #region Fileds
        private readonly IMemoryCache _memoryCache;
        private readonly IApplicationDatabaseContext _context;
        private readonly ILogger<ParserDeleterInit> _logger;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="memoryCache"></param>
        /// <param name="loggerFactory"></param>
        public ParserDeleterInit(
            IMemoryCache memoryCache,
            IApplicationDatabaseContext context,
            ILoggerFactory loggerFactory
        )
        {
            _memoryCache = memoryCache;
            _context = context;
            _logger = loggerFactory.CreateLogger<ParserDeleterInit>();
        }
        #endregion

        #region Methods
        public async Task InitParserDeleter()
        {
            var isInitialised = _memoryCache.Get<IEnumerable<NewsPortal>?>(key: MemoryCacheConstants.NewsPortalKey) != null;
            if (isInitialised)
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

            _ = _memoryCache.Set(
                key: MemoryCacheConstants.NewsPortalKey,
                value: newsPortals.ToList()
            );
            #endregion

            #region Categories
            var categories = await _context
                .Categories
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
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();

            _ = _memoryCache.Set(
                key: MemoryCacheConstants.ArticleKey,
                value: articles
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

            _ = _memoryCache.Set(
                key: MemoryCacheConstants.RssFeedKey,
                value: rssFeeds.ToList()
            );
            #endregion
            stopwatch.Stop();

            var eventId = (int)Event.ParserInit;
            var eventName = Event.ParserInit.GetDisplayName();
            var duration = stopwatch.Elapsed;
            var categoriesCount = categories.Count;
            var newsPortalsCount = newsPortals.Count;
            var allArticlesCount = articles.Count;
            var rssFeedCount = rssFeeds.Count;

            var message =
                $"{AnsiUtility.EncodeEventName($"{{@{nameof(eventName)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(duration))}: " +
                $"{AnsiUtility.EncodeTimespan($"{{@{nameof(duration)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(categoriesCount))}: " +
                $"{AnsiUtility.EncodeRequestParameters($"{{@{nameof(categoriesCount)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(newsPortalsCount))}: " +
                $"{AnsiUtility.EncodeRequestParameters($"{{@{nameof(newsPortalsCount)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(allArticlesCount))}: " +
                $"{AnsiUtility.EncodeRequestParameters($"{{@{nameof(allArticlesCount)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(rssFeedCount))}: " +
                $"{AnsiUtility.EncodeRequestParameters($"{{@{nameof(rssFeedCount)}}}")}\n\t";

            var args = new object[]
            {
                eventName,
                duration,
                categoriesCount,
                newsPortalsCount,
                allArticlesCount,
                rssFeedCount
            };

            _logger.LogInformation(
                eventId: new EventId(id: eventId, name: eventName),
                message: message,
                args: args
            );
        }
        #endregion
    }
}

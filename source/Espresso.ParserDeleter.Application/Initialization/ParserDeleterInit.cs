﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Espresso.Application.IServices;
using Espresso.Application.Services;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Utilities;
using Espresso.Domain.Entities;
using Espresso.Common.Extensions;
using Espresso.Domain.IServices;
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
        private readonly ILoggerService<ParserDeleterInit> _loggerService;
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
            ILoggerService<ParserDeleterInit> loggerService
        )
        {
            _memoryCache = memoryCache;
            _context = context;
            _loggerService = loggerService;
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
                .Include(article => article.MainArticle)
                .ThenInclude(mainArticle => mainArticle!.MainArticle)
                .ThenInclude(article => article!.NewsPortal)
                .Include(article => article.MainArticle)
                .ThenInclude(mainArticle => mainArticle!.MainArticle)
                .ThenInclude(article => article!.ArticleCategories)
                .ThenInclude(articleCategory => articleCategory.Category)
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();

            _memoryCache.Set(
                key: MemoryCacheConstants.ArticleKey,
                value: articles
            );

            var groupedArticles = articles
                .Where(article => article.MainArticle is not null);

            var lastSimilarityGroupingTime = groupedArticles
                .OrderByDescending(groupedArticle => groupedArticle.CreateDateTime)
                .FirstOrDefault()
                ?.CreateDateTime
                ?? new DateTime();

            _memoryCache.Set(MemoryCacheConstants.LastSimilarityGroupingTime, lastSimilarityGroupingTime);
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

            var eventName = Event.ParserInit.GetDisplayName();
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
        #endregion
    }
}

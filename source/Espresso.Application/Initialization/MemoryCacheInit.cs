﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Espresso.Common.Configuration;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.DataAccessLayer.IRepository;
using Espresso.Domain.Entities;
using Espresso.Domain.IServices;
using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.Initialization
{
    /// <summary>
    /// 
    /// </summary>
    public class MemoryCacheInit : IMemoryCacheInit
    {
        #region Fileds
        private readonly IMemoryCache _memoryCache;
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IApplicationDownloadRepository _applicationDownloadRepository;
        private readonly IEspressoDatabaseContext _context;
        private readonly ICommonConfiguration _commonConfiguration;
        private readonly ILoggerService _loggerService;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="memoryCache"></param>
        /// <param name="loggerFactory"></param>
        public MemoryCacheInit(
            IMemoryCache memoryCache,
            IApplicationDownloadRepository applicationDownloadRepository,
            IArticleCategoryRepository articleCategoryRepository,
            IArticleRepository articleRepository,
            IEspressoDatabaseContext context,
            ICommonConfiguration commonConfiguration,
            ILoggerService loggerService
        )
        {
            _memoryCache = memoryCache;
            _articleCategoryRepository = articleCategoryRepository;
            _articleRepository = articleRepository;
            _applicationDownloadRepository = applicationDownloadRepository;
            _context = context;
            _commonConfiguration = commonConfiguration;
            _loggerService = loggerService;
        }
        #endregion

        #region Methods
        public async Task InitWebApi()
        {
            await _context.Database.MigrateAsync();

            var stopwatch = Stopwatch.StartNew();

            #region ApiKeys
            _memoryCache.Set(
                key: MemoryCacheConstants.ApiKeysKey,
                value: _commonConfiguration.ApiKeys.ToList()
            );
            #endregion

            #region NewsPortals
            var newsPortals = await _context
                .NewsPortals
                .AsNoTracking()
                .ToListAsync();

            var newsPortalsDictionary = newsPortals.ToDictionary(newsPortal => newsPortal.Id);

            _memoryCache.Set(
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

            _memoryCache.Set(
                key: MemoryCacheConstants.CategoryKey,
                value: categories.ToList()
            );
            #endregion

            #region Article Categories
            var articleCategories = await _articleCategoryRepository
                .GetArticleCategories()
                .ConfigureAwait(false);
            var articleCategoriesToAdd = new List<ArticleCategory>();

            foreach (var articleCategory in articleCategories)
            {
                if (categoryDictionary.TryGetValue(articleCategory.CategoryId, out var category))
                {
                    articleCategory.SetCategory(category: category);
                    articleCategoriesToAdd.Add(articleCategory);
                }
            }
            #endregion

            #region Articles
            var articleCategoriesDictionary = articleCategoriesToAdd
                .GroupBy(keySelector: articleCategory => articleCategory.ArticleId)
                .ToDictionary(keySelector: grouping => grouping.Key);

            var articles = await _articleRepository.GetArticles();
            var articlesToAdd = new List<Article>();

            foreach (var article in articles)
            {
                if (
                    newsPortalsDictionary.TryGetValue(article.NewsPortalId, out var newsPortal) &&
                    articleCategoriesDictionary.TryGetValue(article.Id, out var articleArticleCategories)
                )
                {
                    article.UpdateNewsPortalAndArticlecategories(
                        newsPortal: newsPortal,
                        articleCategories: articleArticleCategories
                    );
                    articlesToAdd.Add(article);
                }
            }

            _memoryCache.Set(
                key: MemoryCacheConstants.ArticleKey,
                value: articlesToAdd
            );
            #endregion

            #region ApplicationDownloads
            var applicationDownloads = await _applicationDownloadRepository.GetApplicationDownloads();

            _memoryCache.Set(
                key: MemoryCacheConstants.ApplicationDownloadKey,
                value: applicationDownloads.ToList()
            );
            #endregion

            _loggerService.LogWebApiMemoryCacheInit(
                requestId: (int)Event.MemoryCacheInit,
                requestName: nameof(MemoryCacheInit),
                duration: stopwatch.Elapsed,
                categoriesCount: categories.Count,
                newsPortalsCount: newsPortals.Count,
                articleCategoriesCount: articleCategories.Count(),
                totalArticlesCount: articles.Count(),
                articlesCount: articlesToAdd.Count(),
                applicationDownloadsCount: applicationDownloads.Count()
            );
        }

        public async Task InitParserDeleter()
        {
            await _context.Database.MigrateAsync();

            var stopwatch = Stopwatch.StartNew();

            #region ApiKeys
            _memoryCache.Set(
                key: MemoryCacheConstants.ApiKeysKey,
                value: _commonConfiguration.ApiKeys.ToList()
            );
            #endregion

            #region NewsPortals
            var newsPortals = await _context
                .NewsPortals
                .AsNoTracking()
                .ToListAsync();
            var newsPortalsDictionary = newsPortals.ToDictionary(newsPortal => newsPortal.Id);

            _memoryCache.Set(
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

            _memoryCache.Set(
                key: MemoryCacheConstants.CategoryKey,
                value: categories.ToList()
            );
            #endregion

            #region Article Categories
            var articleCategories = await _articleCategoryRepository
                .GetArticleCategories()
                .ConfigureAwait(false);
            var articleCategoriesToAdd = new List<ArticleCategory>();

            foreach (var articleCategory in articleCategories)
            {
                if (categoryDictionary.TryGetValue(articleCategory.CategoryId, out var category))
                {
                    articleCategory.SetCategory(category: category);
                    articleCategoriesToAdd.Add(articleCategory);
                }
            }
            #endregion

            #region Articles
            var articleCategoriesDictionary = articleCategoriesToAdd
                .GroupBy(articleCategory => articleCategory.ArticleId)
                .ToDictionary(grouping => grouping.Key);

            var articles = await _articleRepository.GetArticles();
            var articlesToAdd = new List<Article>();

            foreach (var article in articles)
            {
                if (
                    newsPortalsDictionary.TryGetValue(article.NewsPortalId, out var newsPortal) &&
                    articleCategoriesDictionary.TryGetValue(article.Id, out var articleArticleCategories)
                )
                {
                    article.UpdateNewsPortalAndArticlecategories(
                        newsPortal: newsPortal,
                        articleCategories: articleArticleCategories
                    );
                    articlesToAdd.Add(article);
                }
            }

            _memoryCache.Set(
                key: MemoryCacheConstants.ArticleKey,
                value: articlesToAdd.ToList()
            );
            #endregion

            #region RssFeeds
            var rssFeeds = await _context.RssFeeds
                .Include(rssFeed => rssFeed.Category)
                .Include(rssFeed => rssFeed.NewsPortal)
                .Include(rssFeed => rssFeed.RssFeedCategories)
                .ThenInclude(rssFeedCategory => rssFeedCategory.Category)
                .AsNoTracking()
                .ToListAsync();

            _memoryCache.Set(
                key: MemoryCacheConstants.RssFeedKey,
                value: rssFeeds.ToList()
            );
            #endregion

            _loggerService.LogParserDeleterMemoryCacheInit(
                requestId: (int)Event.MemoryCacheInit,
                requestName: nameof(MemoryCacheInit),
                duration: stopwatch.Elapsed,
                categoriesCount: categories.Count,
                newsPortalsCount: newsPortals.Count,
                articleCategoriesCount: articleCategories.Count(),
                totalArticlesCount: articles.Count(),
                articlesCount: articlesToAdd.Count(),
                rssFeedCount: rssFeeds.Count
            );
        }
        #endregion
    }
}

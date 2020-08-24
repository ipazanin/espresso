using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Espresso.Common.Configuration;
using Espresso.Common.Constants;
using Espresso.Common.Enums;

using Espresso.Domain.Entities;
using Espresso.Domain.IServices;
using Espresso.Persistence.Database;
using Espresso.Persistence.IRepositories;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.Initialization
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationInit : IApplicationInit
    {
        #region Constants
        private const string ConfigurationFileName = "firebase-key.json";

        #endregion

        #region Fileds
        private readonly IMemoryCache _memoryCache;
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IApplicationDownloadRepository _applicationDownloadRepository;
        private readonly IApplicationDatabaseContext _context;
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
        public ApplicationInit(
            IMemoryCache memoryCache,
            IApplicationDownloadRepository applicationDownloadRepository,
            IArticleCategoryRepository articleCategoryRepository,
            IArticleRepository articleRepository,
            IApplicationDatabaseContext context,
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

        #region Public Methods
        public async Task InitWebApi()
        {
            InitializeFireBase();

            await _context.Database.MigrateAsync();

            var stopwatch = Stopwatch.StartNew();

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

            _ = _memoryCache.Set(
                key: MemoryCacheConstants.ArticleKey,
                value: articlesToAdd
            );
            #endregion

            #region ApplicationDownloads
            var applicationDownloads = await _applicationDownloadRepository.GetApplicationDownloads();

            _ = _memoryCache.Set(
                key: MemoryCacheConstants.ApplicationDownloadKey,
                value: applicationDownloads.ToList()
            );
            #endregion

            _loggerService.LogWebApiMemoryCacheInit(
                requestId: (int)Event.MemoryCacheInit,
                requestName: nameof(ApplicationInit),
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

            _ = _memoryCache.Set(
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

            _ = _memoryCache.Set(
                key: MemoryCacheConstants.RssFeedKey,
                value: rssFeeds.ToList()
            );
            #endregion

            _loggerService.LogParserDeleterMemoryCacheInit(
                requestId: (int)Event.MemoryCacheInit,
                requestName: nameof(ApplicationInit),
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

        #region Private Methods
        private void InitializeFireBase()
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

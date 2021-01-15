using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.IServices;
using Espresso.Domain.Records;
using Espresso.Persistence.IRepositories;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Espresso.ParserDeleter.Application.IServices;

namespace Espresso.ParserDeleter.ParseRssFeeds
{
    public class ParseRssFeedsCommandHandler : IRequestHandler<ParseRssFeedsCommand, ParseRssFeedsCommandResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly ISimilarArticleRepository _similarArticleRepository;
        private readonly ICreateArticleService _parseArticlesService;
        private readonly ILoadRssFeedsService _loadRssFeedsService;
        private readonly ISortArticlesService _sortArticlesService;
        private readonly IGroupSimilarArticlesService _groupSimilarArticlesService;
        private readonly ISendArticlesService _sendArticlesService;
        private readonly ILoggerService<ParseRssFeedsCommandHandler> _loggerService;
        #endregion

        #region Constructors
        public ParseRssFeedsCommandHandler(
            IMemoryCache memoryCache,
            IArticleRepository articleRepository,
            IArticleCategoryRepository articleCategoryRepository,
            ISimilarArticleRepository similarArticleRepository,
            ICreateArticleService parseArticlesService,
            ILoadRssFeedsService loadRssFeedsService,
            ISortArticlesService sortArticlesService,
            IGroupSimilarArticlesService groupSimilarArticlesService,
            ISendArticlesService sendArticlesService,
            ILoggerService<ParseRssFeedsCommandHandler> loggerService
        )
        {
            _memoryCache = memoryCache;
            _articleRepository = articleRepository;
            _articleCategoryRepository = articleCategoryRepository;
            _similarArticleRepository = similarArticleRepository;
            _parseArticlesService = parseArticlesService;
            _loadRssFeedsService = loadRssFeedsService;

            _sortArticlesService = sortArticlesService;
            _groupSimilarArticlesService = groupSimilarArticlesService;
            _sendArticlesService = sendArticlesService;
            _loggerService = loggerService;
        }
        #endregion

        #region Methods
        public async Task<ParseRssFeedsCommandResponse> Handle(
            ParseRssFeedsCommand request,
            CancellationToken cancellationToken
        )
        {
            var categories = _memoryCache.Get<IEnumerable<Category>>(key: MemoryCacheConstants.CategoryKey);

            var rssFeeds = _memoryCache.Get<IEnumerable<RssFeed>>(key: MemoryCacheConstants.RssFeedKey);

            var rssFeedItems = await _loadRssFeedsService.ParseRssFeeds(
                rssFeeds: rssFeeds,
                cancellationToken: cancellationToken
            );

            // To Save SkipParseConfiguration.CurrentSkip in Memory Cache Rss Feeds
            _memoryCache.Set(
                key: MemoryCacheConstants.RssFeedKey,
                value: rssFeeds.ToList()
            );

            var articles = await GetArticlesFromLoadedRssFeeds(
                rssFeedItems: rssFeedItems,
                categories: categories,
                cancellationToken: cancellationToken
            );

            var uniqueArticles = _sortArticlesService.RemoveDuplicateArticles(articles);

            var savedArticles = _memoryCache.Get<IDictionary<Guid, Article>>(MemoryCacheConstants.ArticleKey);
            var lastSimilarityGroupingTime = savedArticles.Values.Any() ?
                savedArticles.Values.Max(article => article.CreateDateTime) :
                new DateTime();

            var (createArticles, updateArticles, articleCategoriesToCreate, articleCategoriesToDelete) = _sortArticlesService.SortArticles(
                articles: uniqueArticles,
                savedArticles: savedArticles
            );

            CreateArticles(
                createArticles: createArticles,
                savedArticles: savedArticles
            );
            UpdateArticles(
                updateArticles: updateArticles,
                savedArticles: savedArticles
            );
            _memoryCache.Set(
                key: MemoryCacheConstants.ArticleKey,
                value: savedArticles
            );

            _articleCategoryRepository.InsertArticleCategories(articleCategoriesToCreate);
            _articleCategoryRepository.DeleteArticleCategories(articleCategoriesToDelete.Select(articleCategory => articleCategory.Id));

            CreateSimilarArticles(
                savedArticles: savedArticles,
                lastSimilarityGroupingTime: lastSimilarityGroupingTime
            );

            await _sendArticlesService.SendArticlesMessage(
                createArticles: createArticles,
                updateArticles: updateArticles,
                cancellationToken: cancellationToken
            );

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            return new ParseRssFeedsCommandResponse
            {
                CreatedArticles = createArticles.Count(),
                UpdatedArticles = updateArticles.Count()
            };
        }

        private async Task<IEnumerable<Article>> GetArticlesFromLoadedRssFeeds(
            IEnumerable<RssFeedItem> rssFeedItems,
            IEnumerable<Category> categories,
            CancellationToken cancellationToken
        )
        {
            var initialCapacity = rssFeedItems.Count();
            var parsedArticles = new ConcurrentDictionary<Guid, Article>();

            var tasks = new List<Task>();

            foreach (var rssFeedItem in rssFeedItems)
            {
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        var (article, isValid) = await _parseArticlesService.CreateArticleAsync(
                            rssFeedItem: rssFeedItem,
                            categories: categories,
                            cancellationToken: cancellationToken
                        );

                        if (isValid && article != null)
                        {
                            parsedArticles.TryAdd(article.Id, article);
                        }
                    }
                    catch (Exception exception)
                    {
                        var eventName = "Create Article Unhandled Exception";
                        _loggerService.Log(eventName, exception, LogLevel.Error);
                    }
                }, cancellationToken));
            }

            await Task.WhenAll(tasks);

            return parsedArticles.Values;
        }

        private void CreateArticles(
            IEnumerable<Article> createArticles,
            IDictionary<Guid, Article> savedArticles
        )
        {
            if (!createArticles.Any())
            {
                return;
            }

            var articlesToCreate = new List<Article>();

            foreach (var article in createArticles)
            {
                if (savedArticles.ContainsKey(article.Id))
                {
                    continue;
                }
                savedArticles.Add(article.Id, article);

                articlesToCreate.Add(article);
            }

            _articleRepository.InsertArticles(articlesToCreate);
        }

        private void UpdateArticles(
            IEnumerable<Article> updateArticles,
            IDictionary<Guid, Article> savedArticles
        )
        {
            if (!updateArticles.Any())
            {
                return;
            }

            var articlesToUpdate = new List<Article>();

            foreach (var article in updateArticles)
            {
                if (savedArticles.ContainsKey(article.Id))
                {
                    savedArticles.Remove(article.Id);
                    savedArticles.Add(article.Id, article);
                    articlesToUpdate.Add(article);
                }
            }

            _articleRepository.UpdateArticles(articlesToUpdate);
        }

        private void CreateSimilarArticles(
            IDictionary<Guid, Article> savedArticles,
            DateTime lastSimilarityGroupingTime
        )
        {
            var articles = savedArticles.Values;

            var similarArticles = _groupSimilarArticlesService.GroupSimilarArticles(
                articles,
                lastSimilarityGroupingTime
            );

            _similarArticleRepository.InsertSimilarArticles(similarArticles);

            var articlesDictionary = articles.ToDictionary(article => article.Id);

            foreach (var similarArticle in similarArticles)
            {
                if (
                    articlesDictionary.TryGetValue(similarArticle.MainArticleId, out var mainArticle) &&
                    articlesDictionary.TryGetValue(similarArticle.SubordinateArticleId, out var subordinateArticle)
                )
                {
                    mainArticle.SubordinateArticles.Add(similarArticle);
                    subordinateArticle.SetMainArticle(similarArticle);
                }
            }
        }
        #endregion
    }
}


using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.DataTransferObjects;
using Espresso.Application.Extensions;
using Espresso.Application.IServices;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Common.Extensions;
using Espresso.Domain.IServices;
using Espresso.Domain.Records;
using Espresso.Persistence.IRepositories;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

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
        private readonly ISlackService _slackService;
        private readonly ILoadRssFeedsService _loadRssFeedsService;
        private readonly HttpClient _httpClient;
        private readonly ISortArticlesService _sortArticlesService;
        private readonly IGroupSimilarArticlesService _groupSimilarArticlesService;
        private readonly ILoggerService<ParseRssFeedsCommandHandler> _loggerService;
        #endregion

        #region Constructors
        public ParseRssFeedsCommandHandler(
            IMemoryCache memoryCache,
            IArticleRepository articleRepository,
            IArticleCategoryRepository articleCategoryRepository,
            ISimilarArticleRepository similarArticleRepository,
            ICreateArticleService parseArticlesService,
            ISlackService slackService,
            ILoadRssFeedsService loadRssFeedsService,
            IHttpClientFactory httpClientFactory,
            ISortArticlesService sortArticlesService,
            IGroupSimilarArticlesService groupSimilarArticlesService,
            ILoggerService<ParseRssFeedsCommandHandler> loggerService
        )
        {
            _memoryCache = memoryCache;
            _articleRepository = articleRepository;
            _articleCategoryRepository = articleCategoryRepository;
            _similarArticleRepository = similarArticleRepository;
            _parseArticlesService = parseArticlesService;
            _slackService = slackService;
            _loadRssFeedsService = loadRssFeedsService;

            _httpClient = httpClientFactory.CreateClient();
            _httpClient.Timeout = TimeSpan.FromMinutes(4);

            _sortArticlesService = sortArticlesService;
            _groupSimilarArticlesService = groupSimilarArticlesService;
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
                appEnvironment: request.AppEnvironment,
                currentApiVersion: request.CurrentApiVersion,
                cancellationToken: cancellationToken
            );

            // To Save SkipParseConfiguration.CurrentSkip
            _ = _memoryCache.Set(
                key: MemoryCacheConstants.RssFeedKey,
                value: rssFeeds.ToList()
            );

            var articles = await GetArticlesFromLoadedRssFeeds(
                rssFeedItems: rssFeedItems,
                categories: categories,
                request: request,
                cancellationToken: cancellationToken
            );

            var uniqueArticles = _sortArticlesService.RemoveDuplicateArticles(articles);

            var (createArticles, updateArticles, articleCategoriesToCreate, articleCategoriesToDelete) = _sortArticlesService.SortArticles(
                articles: uniqueArticles,
                savedArticles: _memoryCache.Get<IEnumerable<Article>>(MemoryCacheConstants.ArticleKey)
            );

            CreateArticles(articles: createArticles);
            UpdateArticles(articles: updateArticles);

            _articleCategoryRepository.InsertArticleCategories(articleCategoriesToCreate);
            _articleCategoryRepository.DeleteArticleCategories(articleCategoriesToDelete.Select(articleCategory => articleCategory.Id));

            CreateSimilarArticles();

            await CallWebServer(
                request: request,
                createArticles: createArticles,
                updateArticles: updateArticles,
                cancellationToken: cancellationToken
            );

            return new ParseRssFeedsCommandResponse
            {
                CreatedArticles = createArticles.Count(),
                UpdatedArticles = updateArticles.Count()
            };
        }

        public async Task<IEnumerable<Article>> GetArticlesFromLoadedRssFeeds(
            IEnumerable<RssFeedItem> rssFeedItems,
            IEnumerable<Category> categories,
            ParseRssFeedsCommand request,
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
                            maxAgeOfArticle: request.MaxAgeOfArticle,
                            cancellationToken: cancellationToken
                        );

                        if (isValid && article != null)
                        {
                            parsedArticles.TryAdd(article.Id, article);
                        }
                    }
                    catch (Exception exception)
                    {
                        var eventName = "CreatecArticle Unhandled Exception";
                        _loggerService.Log(eventName, exception, LogLevel.Error);
                    }
                }, cancellationToken));
            }

            await Task.WhenAll(tasks);

            return parsedArticles.Values;
        }

        private void CreateArticles(
            IEnumerable<Article> articles
        )
        {
            if (!articles.Any())
            {
                return;
            }

            var articlesDictionary = _memoryCache
                .Get<IEnumerable<Article>>(MemoryCacheConstants.ArticleKey)
                .ToDictionary(article => article.Id);

            var articlesToCreate = new List<Article>();

            foreach (var article in articles)
            {
                if (articlesDictionary.ContainsKey(article.Id))
                {
                    continue;
                }
                articlesDictionary.Add(article.Id, article);

                articlesToCreate.Add(article);
            }

            _articleRepository.InsertArticles(articlesToCreate);

            _ = _memoryCache.Set(
                key: MemoryCacheConstants.ArticleKey,
                value: articlesDictionary.Values.ToList()
            );
        }

        private void UpdateArticles(
            IEnumerable<Article> articles
        )
        {
            if (!articles.Any())
            {
                return;
            }

            var articlesDictionary = _memoryCache
                .Get<IEnumerable<Article>>(MemoryCacheConstants.ArticleKey)
                .ToDictionary(article => article.Id);

            var articlesToUpdate = new List<Article>();

            foreach (var article in articles)
            {
                if (articlesDictionary.ContainsKey(article.Id))
                {
                    _ = articlesDictionary.Remove(article.Id);
                    articlesDictionary.Add(article.Id, article);
                    articlesToUpdate.Add(article);
                }
            }

            _articleRepository.UpdateArticles(articlesToUpdate);

            _ = _memoryCache.Set(
                key: MemoryCacheConstants.ArticleKey,
                value: articlesDictionary.Values.ToList()
            );
        }

        private void CreateSimilarArticles()
        {
            var articles = _memoryCache.Get<IEnumerable<Article>>(key: MemoryCacheConstants.ArticleKey);
            var lastSimilarityGroupingTime = _memoryCache.Get<DateTime>(key: MemoryCacheConstants.LastSimilarityGroupingTime);

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

            _memoryCache.Set(key: MemoryCacheConstants.ArticleKey, value: articlesDictionary.Values.ToList());
            _memoryCache.Set(key: MemoryCacheConstants.LastSimilarityGroupingTime, value: DateTime.UtcNow);
        }

        private async Task CallWebServer(
            ParseRssFeedsCommand request,
            IEnumerable<Article> createArticles,
            IEnumerable<Article> updateArticles,
            CancellationToken cancellationToken
        )
        {
            if (!createArticles.Any() && !updateArticles.Any())
            {
                return;
            }

            var createdArticleIds = createArticles.Select(article => article.Id);
            var updatedArticleIds = updateArticles.Select(article => article.Id);

            var httpHeaders = new List<(string headerKey, string headerValue)>
            {
                (headerKey: HttpHeaderConstants.ApiKeyHeaderName, headerValue: request.ParserApiKey),
                (headerKey: HttpHeaderConstants.EspressoApiHeaderName, headerValue: request.TargetedApiVersion),
                (headerKey: HttpHeaderConstants.VersionHeaderName, headerValue: request.CurrentApiVersion),
                (headerKey: HttpHeaderConstants.DeviceTypeHeaderName, headerValue: ((int)DeviceType.RssFeedParser).ToString()),
            };

            try
            {
                _httpClient.AddHeadersToHttpClient(httpHeaders);
                await _httpClient.PostAsJsonAsync(
                    requestUri: $"{request.ServerUrl}/api/notifications/articles",
                    value: new ArticlesBodyDto
                    {
                        CreatedArticles = createdArticleIds,
                        UpdatedArticleIds = updatedArticleIds
                    },
                    cancellationToken: cancellationToken
                );

                return;
            }
            catch (Exception exception)
            {
                var eventName = Event.SendNewAndUpdatedArticlesRequest.GetDisplayName();
                var version = request.CurrentApiVersion;
                var arguments = new (string parameterName, object parameterValue)[]
                {
                    (nameof(version), version)
                };

                _loggerService.Log(eventName, exception, LogLevel.Error, arguments);

                await _slackService.LogError(
                        eventName: eventName,
                        version: request.TargetedApiVersion,
                        message: exception.Message,
                        exception: exception,
                        appEnvironment: request.AppEnvironment,
                        cancellationToken: default
                );
            }
        }
        #endregion
    }
}


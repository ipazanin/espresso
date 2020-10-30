using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.DataTransferObjects;
using Espresso.Application.IServices;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Utilities;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Domain.Extensions;
using Espresso.Domain.Records;
using Espresso.ParserDeleter.Application.IServices;
using Espresso.Persistence.IRepositories;
using Espresso.Wepi.Application.IServices;
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
        private readonly ICreateArticleService _parseArticlesService;
        private readonly ISlackService _slackService;
        private readonly ILoadRssFeedsService _loadRssFeedsService;
        private readonly IHttpService _httpService;
        private readonly ISortArticlesService _sortArticlesService;
        private readonly ILogger<ParseRssFeedsCommandHandler> _logger;
        #endregion

        #region Constructors
        public ParseRssFeedsCommandHandler(
            IMemoryCache memoryCache,
            IArticleRepository articleRepository,
            IArticleCategoryRepository articleCategoryRepository,
            ICreateArticleService parseArticlesService,
            ISlackService slackService,
            ILoggerFactory loggerFactory,
            ILoadRssFeedsService loadRssFeedsService,
            IHttpService httpService,
            ISortArticlesService sortArticlesService
        )
        {
            _memoryCache = memoryCache;
            _articleRepository = articleRepository;
            _articleCategoryRepository = articleCategoryRepository;
            _parseArticlesService = parseArticlesService;
            _slackService = slackService;
            _loadRssFeedsService = loadRssFeedsService;
            _httpService = httpService;
            _sortArticlesService = sortArticlesService;
            _logger = loggerFactory.CreateLogger<ParseRssFeedsCommandHandler>();
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
                        var eventName = "CreateArticleUnhandledException";
                        var message = exception.Message;
                        _logger.LogError(
                            message: $"{AnsiUtility.EncodeEventName("{0}")}\n\t" +
                                $"{AnsiUtility.EncodeParameterName(nameof(message))}: " +
                                $"{AnsiUtility.EncodeErrorMessage("{1}")}\n\t",
                            args: new object[]
                            {
                                eventName,
                                message
                            }
                        );
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

            var savedArticles = _memoryCache
                .Get<IEnumerable<Article>>(MemoryCacheConstants.ArticleKey)
                .ToDictionary(article => article.Id);

            var articlesToCreate = new List<Article>();

            foreach (var article in articles)
            {
                if (savedArticles.ContainsKey(article.Id))
                {
                    continue;
                }
                savedArticles.Add(article.Id, article);

                articlesToCreate.Add(article);
            }

            _articleRepository.InsertArticles(articlesToCreate);

            _ = _memoryCache.Set(
                key: MemoryCacheConstants.ArticleKey,
                value: savedArticles.Values.ToList()
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

            var savedArticles = _memoryCache
                .Get<IEnumerable<Article>>(MemoryCacheConstants.ArticleKey)
                .ToDictionary(article => article.Id);

            var articlesToUpdate = new List<Article>();

            foreach (var article in articles)
            {
                if (savedArticles.ContainsKey(article.Id))
                {
                    _ = savedArticles.Remove(article.Id);
                    savedArticles.Add(article.Id, article);
                    articlesToUpdate.Add(article);
                }
            }

            _articleRepository.UpdateArticles(articlesToUpdate);

            _ = _memoryCache.Set(
                key: MemoryCacheConstants.ArticleKey,
                value: savedArticles.Values.ToList()
            );
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

            var createdArticleDtos = createArticles.Select(ArticleDto.GetProjection().Compile());
            var updatedArticleDtos = updateArticles.Select(ArticleDto.GetProjection().Compile());

            var httpHeaders = new List<(string headerKey, string headerValue)>
            {
                (headerKey: HttpHeaderConstants.ApiKeyHeaderName, headerValue: request.ParserApiKey),
                (headerKey: HttpHeaderConstants.EspressoApiHeaderName, headerValue: request.TargetedApiVersion),
                (headerKey: HttpHeaderConstants.VersionHeaderName, headerValue: request.CurrentApiVersion),
                (headerKey: HttpHeaderConstants.DeviceTypeHeaderName, headerValue: ((int)DeviceType.RssFeedParser).ToString()),
            };

            try
            {
                await _httpService.PostJsonAsync(
                    url: $"{request.ServerUrl}/api/notifications/articles",
                    data: new ArticlesBodyDto
                    {
                        CreatedArticles = createdArticleDtos,
                        UpdatedArticles = updatedArticleDtos
                    },
                    httpHeaders: httpHeaders,
                    httpClientTimeout: TimeSpan.FromSeconds(30),
                    cancellationToken: cancellationToken
                );

                return;
            }
            catch (Exception exception)
            {
                var eventName = Event.SendNewAndUpdatedArticlesRequest.GetDisplayName();
                var eventId = (int)Event.SendNewAndUpdatedArticlesRequest;
                var version = request.CurrentApiVersion;
                var exceptionMessage = exception.Message;
                var innerExceptionMessage = exception.InnerException?.Message ?? FormatConstants.EmptyValue;
                _logger.LogError(
                    eventId: new EventId(
                        id: eventId,
                        name: eventName
                    ),
                    exception: exception,
                    message: $"{AnsiUtility.EncodeEventName("{0}")}\n\t" +
                        $"{AnsiUtility.EncodeParameterName(nameof(version))}: " +
                        $"{AnsiUtility.EncodeVersion("{1}")}\n\t" +
                        $"{AnsiUtility.EncodeParameterName(nameof(exceptionMessage))}: " +
                        $"{AnsiUtility.EncodeErrorMessage("{2}")}\n\t" +
                        $"{AnsiUtility.EncodeParameterName(nameof(innerExceptionMessage))}: " +
                        $"{AnsiUtility.EncodeErrorMessage("{3}")}",
                    args: new object[]
                    {
                            eventName,
                            version,
                            exceptionMessage,
                            innerExceptionMessage,
                    }
                );
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


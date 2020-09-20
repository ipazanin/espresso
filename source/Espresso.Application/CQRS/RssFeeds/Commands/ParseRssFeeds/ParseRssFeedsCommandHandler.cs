using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.DataTransferObjects;
using Espresso.Application.IService;
using Espresso.Application.IServices;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Utilities;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Domain.Extensions;
using Espresso.Persistence.IRepositories;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Espresso.Application.CQRS.RssFeeds.Commands.ParseRssFeeds
{
    public class ParseRssFeedsCommandHandler : IRequestHandler<ParseRssFeedsCommand>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IArticleParserService _articleParserService;
        private readonly ISlackService _slackService;
        private readonly IRssFeedLoadService _rssFeedLoadingService;
        private readonly IHttpService _httpService;
        private readonly ILogger<ParseRssFeedsCommandHandler> _logger;
        #endregion

        #region Constructors
        public ParseRssFeedsCommandHandler(
            IMemoryCache memoryCache,
            IArticleRepository articleRepository,
            IArticleCategoryRepository articleCategoryRepository,
            IArticleParserService articleParserService,
            ISlackService slackService,
            ILoggerFactory loggerFactory,
            IRssFeedLoadService rssFeedService,
            IHttpService httpService
        )
        {
            _memoryCache = memoryCache;
            _articleRepository = articleRepository;
            _articleCategoryRepository = articleCategoryRepository;
            _articleParserService = articleParserService;
            _slackService = slackService;
            _rssFeedLoadingService = rssFeedService;
            _httpService = httpService;
            _logger = loggerFactory.CreateLogger<ParseRssFeedsCommandHandler>();
        }
        #endregion

        #region Methods

        #region Public Methods
        public async Task<Unit> Handle(
            ParseRssFeedsCommand request,
            CancellationToken cancellationToken
        )
        {
            var categories = _memoryCache.Get<IEnumerable<Category>>(key: MemoryCacheConstants.CategoryKey);

            var rssFeeds = _memoryCache.Get<IEnumerable<RssFeed>>(key: MemoryCacheConstants.RssFeedKey);
            var newsPortals = _memoryCache.Get<IEnumerable<NewsPortal>>(key: MemoryCacheConstants.NewsPortalKey);

            var feeds = await _rssFeedLoadingService.ParseRssFeeds(
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
                feeds: feeds,
                categories: categories,
                request: request,
                cancellationToken: cancellationToken
            );

            var (createdArticles, updatedArticles) = CreateOrUpdateArticles(
                articles: articles,
                newsPortals: newsPortals,
                categories: categories
            );

            await CallWebServer(
                request: request,
                createdArticles: createdArticles,
                updatedArticles: updatedArticles,
                cancellationToken: cancellationToken
            );

            return Unit.Value;
        }
        #endregion

        #region Private Methods
        public async Task<IEnumerable<Article>> GetArticlesFromLoadedRssFeeds(
            IEnumerable<(SyndicationFeed syndicationFeed, RssFeed rssFeed)> feeds,
            IEnumerable<Category> categories,
            ParseRssFeedsCommand request,
            CancellationToken cancellationToken
        )
        {
            var initialCapacity = feeds.Count();
            var parsedArticles = new ConcurrentDictionary<Guid, Article>();
            var articleIdArticleDictionary = new Dictionary<(int newsPortalId, string articleId), Guid>(initialCapacity);
            var titleArticleDictionary = new Dictionary<(int newsPortalId, string title), Guid>(initialCapacity);
            var summaryArticleDictionary = new Dictionary<(int newsPortalId, string summary), Guid>(initialCapacity);

            var tasks = new List<Task>();
            var random = new Random();

            foreach (var (syndicationFeed, rssFeed) in feeds)
            {
                foreach (var syndicationItem in syndicationFeed.Items)
                {
                    if (syndicationItem is null)
                    {
                        continue;
                    }

                    tasks.Add(Task.Run(async () =>
                    {
                        var initialNumberOfClicks = random.Next(0, 15);
                        try
                        {
                            var article = await _articleParserService.CreateArticleAsync(
                                rssFeed: rssFeed,
                                categories: categories,
                                itemId: syndicationItem.Id,
                                itemLinks: syndicationItem.Links?.Select(syndicationLink => syndicationLink.Uri),
                                itemTitle: syndicationItem.Title?.Text,
                                itemSummary: syndicationItem.Summary?.Text,
                                itemContent: (syndicationItem.Content as TextSyndicationContent)?.Text,
                                itemPublishDateTime: syndicationItem.PublishDate,
                                maxAgeOfArticle: request.MaxAgeOfArticle,
                                elementExtensions: syndicationItem.ElementExtensions?.Select(elementExtension => elementExtension?.GetObject<string?>()),
                                cancellationToken: cancellationToken
                            );

                            _ = parsedArticles.TryAdd(article.Id, article);
                        }
                        catch (Exception exception)
                        {
                            var rssFeedUrl = rssFeed.Url;
                            var exceptionMessage = exception.Message;
                            var innerExceptionMessage = exception.InnerException?.Message ?? FormatConstants.EmptyValue;
                            var eventName = Event.ArticleParsing.GetDisplayName();
                            var eventId = (int)Event.ArticleParsing;
                            var message = $"RssFeedUrl: {rssFeedUrl}";

                            if (exception.Message.Equals("articleCategories must not be empty! (Parameter 'articleCategories')"))
                            {
                                var urlCategories = string.Join(
                                    separator: ", ",
                                    values: rssFeed.RssFeedCategories?
                                        .Select(rssFeedCategory => $"{rssFeedCategory.UrlRegex}-{rssFeedCategory.UrlSegmentIndex}:{rssFeedCategory.Category?.Name ?? ""}")
                                        ?? Array.Empty<string>()
                                );
                                await _slackService.LogMissingCategoriesError(
                                    version: request.CurrentApiVersion,
                                    rssFeedUrl: rssFeedUrl,
                                    articleUrl: syndicationItem?.Links?.FirstOrDefault()?.Uri?.ToString() ?? "",
                                    urlCategories: urlCategories,
                                    appEnvironment: request.AppEnvironment,
                                    cancellationToken: cancellationToken
                                );
                            }
                            else if (exception.Message.Equals("publishDateTime must not be empty! (Parameter 'publishDateTime')"))
                            {

                            }
                            else
                            {
                                _logger.LogWarning(
                                    eventId: new EventId(id: eventId, name: eventName),
                                    message: $"{AnsiUtility.EncodeEventName("{0}")}\n\t" +
                                        $"{AnsiUtility.EncodeParameterName(nameof(message))}: " +
                                        $"{AnsiUtility.EncodeRequestParameters("{1}")}\n\t" +
                                        $"{AnsiUtility.EncodeParameterName(nameof(exceptionMessage))}: " +
                                        $"{AnsiUtility.EncodeErrorMessage("{2}")}\n\t" +
                                        $"{AnsiUtility.EncodeParameterName(nameof(innerExceptionMessage))}: " +
                                        $"{AnsiUtility.EncodeErrorMessage("{3}")}",
                                    args: new object[]
                                    {
                                        eventName,
                                        message,
                                        exceptionMessage,
                                        innerExceptionMessage,
                                    }
                                );

                                await _slackService.LogWarning(
                                    eventName: eventName,
                                    version: request.CurrentApiVersion,
                                    message: message,
                                    exception: exception,
                                    appEnvironment: request.AppEnvironment,
                                    cancellationToken: cancellationToken
                                );
                            }
                        }
                    }, cancellationToken));
                }
            }

            await Task.WhenAll(tasks);

            var uniqueArticles = new Dictionary<Guid, Article>(parsedArticles.Count);

            foreach (var article in parsedArticles.Values)
            {
                if (
                    articleIdArticleDictionary.TryGetValue((article.NewsPortalId, article.Url), out var alreadyParsedArticleId) ||
                    titleArticleDictionary.TryGetValue((article.NewsPortalId, article.Title), out alreadyParsedArticleId) ||
                    summaryArticleDictionary.TryGetValue((article.NewsPortalId, article.Summary), out alreadyParsedArticleId)
                )
                {
                    if (uniqueArticles.TryGetValue(alreadyParsedArticleId, out var parsedArticle))
                    {
                        parsedArticle.UpdateArticleCategories(article.ArticleCategories);
                    }
                }
                else
                {
                    _ = uniqueArticles.TryAdd(article.Id, article);
                    _ = articleIdArticleDictionary.TryAdd((article.NewsPortalId, article.Url), article.Id);
                    _ = titleArticleDictionary.TryAdd((article.NewsPortalId, article.Title), article.Id);
                    _ = summaryArticleDictionary.TryAdd((article.NewsPortalId, article.Summary), article.Id);
                }
            }

            return uniqueArticles.Values;
        }

        private (IEnumerable<ArticleDto> createdArticles, IEnumerable<ArticleDto> updatedArticles) CreateOrUpdateArticles(
            IEnumerable<Article> articles,
            IEnumerable<NewsPortal> newsPortals,
            IEnumerable<Category> categories
        )
        {
            _ = _memoryCache.Set(
                key: MemoryCacheConstants.DeadLockLogKey,
                value: $"Started {nameof(CreateOrUpdateArticles)}"
            );
            var createArticles = new List<Article>();
            var updateArticles = new List<Article>();
            var savedArticles = _memoryCache.Get<IEnumerable<Article>>(
                key: MemoryCacheConstants.ArticleKey
            );

            var savedArticlesIdDictionary = savedArticles.ToDictionary(
                keySelector: article => article.Id
            );

            var savedArticlesArticleIdDictionary = new Dictionary<(int newsPortalId, string articleId), Guid>();
            var savedArticlesTitleDictionary = new Dictionary<(int newsPortalId, string title), Guid>();
            var savedArticlesSummaryDictionary = new Dictionary<(int newsPortalId, string summary), Guid>();

            foreach (var article in savedArticles)
            {
                _ = savedArticlesArticleIdDictionary.TryAdd((article.NewsPortalId, article.Url), article.Id);
                _ = savedArticlesTitleDictionary.TryAdd((article.NewsPortalId, article.Title), article.Id);
                _ = savedArticlesSummaryDictionary.TryAdd((article.NewsPortalId, article.Summary), article.Id);
            }

            foreach (var article in articles)
            {
                if (
                    savedArticlesArticleIdDictionary.TryGetValue((article.NewsPortalId, article.Url), out var savedArticleId) ||
                    savedArticlesTitleDictionary.TryGetValue((article.NewsPortalId, article.Title), out savedArticleId) ||
                    savedArticlesSummaryDictionary.TryGetValue((article.NewsPortalId, article.Summary), out savedArticleId)
                )
                {
                    var savedArticle = savedArticlesIdDictionary[savedArticleId];

                    if (savedArticle.Update(article))
                    {
                        updateArticles.Add(savedArticle);
                    }
                }
                else
                {
                    createArticles.Add(article);
                }
            }

            CreateArticles(articles: createArticles);

            UpdateArticles(articles: updateArticles);

            var createdArticleDtos = createArticles.Select(ArticleDto.GetProjection().Compile());
            var updatedArticleDtos = updateArticles.Select(ArticleDto.GetProjection().Compile());

            _ = _memoryCache.Set(
                key: MemoryCacheConstants.DeadLockLogKey,
                value: $"Ended {nameof(CreateOrUpdateArticles)}"
            );

            return (createdArticleDtos, updatedArticleDtos);
        }

        private void CreateArticles(
            IEnumerable<Article> articles
        )
        {
            if (!articles.Any())
            {
                return;
            }
            _ = _memoryCache.Set(
                key: MemoryCacheConstants.DeadLockLogKey,
                value: $"Started {nameof(CreateArticles)}"
            );

            var savedArticles = _memoryCache
                .Get<IEnumerable<Article>>(MemoryCacheConstants.ArticleKey)
                .ToDictionary(article => article.Id);

            var articlesToCreate = new List<Article>();
            var articleCategoriesToCreate = new List<ArticleCategory>();

            foreach (var article in articles)
            {
                if (savedArticles.ContainsKey(article.Id))
                {
                    continue;
                }
                savedArticles.Add(article.Id, article);

                articlesToCreate.Add(article);
                articleCategoriesToCreate.AddRange(article.ArticleCategories);
            }

            _articleRepository.InsertArticles(articlesToCreate);
            _articleCategoryRepository.InsertArticleCategories(articleCategoriesToCreate);

            _ = _memoryCache.Set(
                key: MemoryCacheConstants.ArticleKey,
                value: savedArticles.Values.ToList()
            );
            _ = _memoryCache.Set(
                key: MemoryCacheConstants.DeadLockLogKey,
                value: $"Ended {nameof(CreateArticles)}"
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
            _ = _memoryCache.Set(
                key: MemoryCacheConstants.DeadLockLogKey,
                value: $"Started {nameof(UpdateArticles)}"
            );

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
            var createArticleCategories = articlesToUpdate
                .SelectMany(article => article.CreateArticleCategories);

            var deleteArticleCategoryIds = articlesToUpdate
                .SelectMany(
                    article => article
                        .DeleteArticleCategories
                        .Select(deleteArticleCategory => deleteArticleCategory.Id)
                );

            _articleRepository.UpdateArticles(articlesToUpdate);
            _articleCategoryRepository.InsertArticleCategories(createArticleCategories);
            _articleCategoryRepository.DeleteArticleCategories(deleteArticleCategoryIds);

            _ = _memoryCache.Set(
                key: MemoryCacheConstants.ArticleKey,
                value: savedArticles.Values.ToList()
            );
            _ = _memoryCache.Set(
                key: MemoryCacheConstants.DeadLockLogKey,
                value: $"Ended {nameof(UpdateArticles)}"
            );
        }

        private async Task CallWebServer(
            ParseRssFeedsCommand request,
            IEnumerable<ArticleDto> createdArticles,
            IEnumerable<ArticleDto> updatedArticles,
            CancellationToken cancellationToken
        )
        {
            if (!createdArticles.Any() && !updatedArticles.Any())
            {
                return;
            }

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
                    data: new ArticlesRequestObjectDto
                    {
                        CreatedArticles = createdArticles,
                        UpdatedArticles = updatedArticles
                    },
                    httpHeaders: httpHeaders,
                    httpClientTimeout: TimeSpan.FromSeconds(30),
                    cancellationToken: cancellationToken
                );

                return;
            }
            catch (Exception exception)
            {
                var eventName = Event.ParserDeleterNewArticlesRequest.GetDisplayName();
                var eventId = (int)Event.ParserDeleterNewArticlesRequest;
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
                await Task.Delay(request.WaitDurationAfterWebServerRequestError, cancellationToken);
            }
        }
        #endregion

        #endregion
    }
}


using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Espresso.Application.DataTransferObjects;
using Espresso.Common.Configuration;
using Espresso.Common.Constants;
using Espresso.Common.Enums;

using Espresso.Domain.Entities;
using Espresso.Domain.Enums.NewsPortalEnums;
using Espresso.Domain.Extensions;
using Espresso.Domain.IServices;
using Espresso.Persistence.Database;
using Espresso.Persistence.IRepositories;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.CQRS.RssFeeds.Commands.ParseRssFeeds
{
    public class ParseRssFeedsCommandHandler : IRequestHandler<ParseRssFeedsCommand, ParseRssFeedsCommandResponse>
    {
        #region Fields
        private readonly ILoggerService _loggerService;
        private readonly IMemoryCache _memoryCache;
        private readonly IApplicationDatabaseContext _context;
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IArticleParserService _articleParserService;
        private readonly ICommonConfiguration _commonConfiguration;
        private readonly HttpClient _httpClient;
        #endregion

        #region Constructors
        public ParseRssFeedsCommandHandler(
            IHttpClientFactory httpClientFactory,
            ILoggerService loggerService,
            IMemoryCache memoryCache,
            IApplicationDatabaseContext context,
            IArticleRepository articleRepository,
            IArticleCategoryRepository articleCategoryRepository,
            IArticleParserService articleParserService,
            ICommonConfiguration commonConfiguration
        )
        {
            _loggerService = loggerService;
            _memoryCache = memoryCache;
            _context = context;
            _articleRepository = articleRepository;
            _articleCategoryRepository = articleCategoryRepository;
            _articleParserService = articleParserService;
            _commonConfiguration = commonConfiguration;
            _httpClient = httpClientFactory.CreateClient();
        }
        #endregion

        #region Methods

        #region Public Methods
        public async Task<ParseRssFeedsCommandResponse> Handle(
            ParseRssFeedsCommand request,
            CancellationToken cancellationToken
        )
        {
            var categories = _memoryCache.Get<IEnumerable<Category>>(key: MemoryCacheConstants.CategoryKey);

            var rssFeeds = _memoryCache.Get<IEnumerable<RssFeed>>(key: MemoryCacheConstants.RssFeedKey);
            var newsPortals = _memoryCache.Get<IEnumerable<NewsPortal>>(key: MemoryCacheConstants.NewsPortalKey);

            var feeds = await ParseRssFeeds(
                rssFeeds: rssFeeds,
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

            // To Save SkipParseConfiguration.CurrentSkip
            _ = _memoryCache.Set(
                key: MemoryCacheConstants.RssFeedKey,
                value: rssFeeds.ToList()
            );

            var articles = await GetArticlesFromLoadedRssFeeds(
                feeds: feeds,
                categories: categories,
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

            var (createdArticles, updatedArticles) = CreateOrUpdateArticles(
                articles: articles,
                newsPortals: newsPortals,
                categories: categories
            );

            return new ParseRssFeedsCommandResponse(createdArticles, updatedArticles);
        }
        #endregion

        #region Private Methods
        private async Task<IEnumerable<(SyndicationFeed SyndicationFeed, RssFeed rssFeed)>> ParseRssFeeds(
            IEnumerable<RssFeed> rssFeeds,
            CancellationToken cancellationToken
        )
        {
            _ = _memoryCache.Set(
                key: MemoryCacheConstants.DeadLockLogKey,
                value: $"Started {nameof(ParseRssFeeds)}"
            );
            var parsedArticles = new ConcurrentQueue<(SyndicationFeed SyndicationFeed, RssFeed rssFeed)>();

            var getRssFeedRequestTasks = new List<Task>();

            foreach (var rssFeed in rssFeeds)
            {
                var closureRssFeed = rssFeed;
                if (!closureRssFeed.ShouldParse())
                {
                    continue;
                }

                getRssFeedRequestTasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        SyndicationFeed feed;
                        // TODO refactor this so it is not hardcoded for this portal
                        if (
                            closureRssFeed.NewsPortalId == (int)NewsPortalId.SportskeNovosti ||
                            closureRssFeed.NewsPortalId == (int)NewsPortalId.JutarnjiList ||
                            closureRssFeed.NewsPortalId == (int)NewsPortalId.NarodHr ||
                            closureRssFeed.NewsPortalId == (int)NewsPortalId.StoPosto ||
                            closureRssFeed.NewsPortalId == (int)NewsPortalId.PoslovniPuls ||
                            closureRssFeed.NewsPortalId == (int)NewsPortalId.SlobodnaDalmacija ||
                            closureRssFeed.NewsPortalId == (int)NewsPortalId.LikaExpress
                        )
                        {
                            using var request = new HttpRequestMessage(HttpMethod.Get, closureRssFeed.Url);
                            _ = request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
                            _ = request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
                            _ = request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                            _ = request.Headers.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

                            using var response = await _httpClient
                                .SendAsync(request, cancellationToken)
                                .ConfigureAwait(false);
                            _ = response.EnsureSuccessStatusCode();
                            using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                            using var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress);
                            using var streamReader = new StreamReader(decompressedStream);
                            var stringContent = await streamReader.ReadToEndAsync().ConfigureAwait(false);
                            var reader = XmlReader.Create(new StringReader(stringContent.Replace("version=\"2.0\" version=\"2.0\"", "version=\"2.0\"")));
                            feed = SyndicationFeed.Load(reader);
                        }
                        else
                        {
                            using var reader = XmlReader.Create(closureRssFeed.Url);
                            feed = SyndicationFeed.Load(reader);
                        }

                        parsedArticles.Enqueue((feed, closureRssFeed));
                    }
                    catch (Exception exception)
                    {
                        var rssFeedUrl = closureRssFeed.Url;
                        var exceptionMessage = exception.Message;

                        await _loggerService.LogWarning(
                            eventId: (int)Event.RssFeedLoading,
                            eventName: Event.RssFeedLoading.GetDisplayName(),
                            version: _commonConfiguration.Version,
                            message: $"RssFeedUrl: {rssFeedUrl}",
                            exception: exception,
                            cancellationToken: cancellationToken
                        ).ConfigureAwait(false);
                    }
                }, cancellationToken));
            }

            await Task.WhenAll(getRssFeedRequestTasks).ConfigureAwait(false);

            _ = _memoryCache.Set(
                key: MemoryCacheConstants.DeadLockLogKey,
                        value: $"Ended {nameof(ParseRssFeeds)}"
                    );

            return parsedArticles;
        }

        public async Task<IEnumerable<Article>> GetArticlesFromLoadedRssFeeds(
            IEnumerable<(SyndicationFeed syndicationFeed, RssFeed rssFeed)> feeds,
            IEnumerable<Category> categories,
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
                                summary: syndicationItem.Summary?.Text,
                                itemContent: (syndicationItem.Content as TextSyndicationContent)?.Text,
                                publishDateTime: syndicationItem.PublishDate,
                                cancellationToken: cancellationToken
                            ).ConfigureAwait(false);

                            _ = parsedArticles.TryAdd(article.Id, article);
                        }
                        catch (Exception exception)
                        {
                            var rssFeedUrl = rssFeed.Url;
                            var exceptionMessage = exception.Message;

                            if (exception.Message.Equals("articleCategories must not be empty! (Parameter 'articleCategories')"))
                            {
                                var urlcategories = string.Join(
                                    separator: ", ",
                                    values: rssFeed.RssFeedCategories?
                                        .Select(rssFeedCategory => $"{rssFeedCategory.UrlRegex}-{rssFeedCategory.UrlSegmentIndex}:{rssFeedCategory.Category?.Name ?? ""}")
                                        ?? new string[] { }
                                );
                                await _loggerService.LogMissingCategoriesError(
                                    version: _commonConfiguration.Version,
                                    rssFeedUrl: rssFeedUrl,
                                    articleUrl: syndicationItem?.Links?.FirstOrDefault()?.Uri?.ToString() ?? "",
                                    urlCategories: urlcategories,
                                    cancellationToken: cancellationToken
                                );

                            }
                            else if (exception.Message.Equals("publishDateTime must not be empty! (Parameter 'publishDateTime')"))
                            {

                            }
                            else
                            {

                                await _loggerService.LogWarning(
                                    eventId: (int)Event.ArticleParsing,
                                    eventName: Event.ArticleParsing.GetDisplayName(),
                                    version: _commonConfiguration.Version,
                                    message: $"RssFeedUrl: {rssFeedUrl}",
                                    exception: exception,
                                    cancellationToken: cancellationToken
                                ).ConfigureAwait(false);
                            }
                        }
                    }));
                }
            }

            await Task.WhenAll(tasks)
                .ConfigureAwait(false);

            var uniqueArticles = new Dictionary<Guid, Article>(parsedArticles.Count);

            foreach (var article in parsedArticles.Values)
            {
                if (
                    articleIdArticleDictionary.TryGetValue((article.NewsPortalId, article.ArticleId), out var alreadyParsedArticleId) ||
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
                    _ = articleIdArticleDictionary.TryAdd((article.NewsPortalId, article.ArticleId), article.Id);
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
                _ = savedArticlesArticleIdDictionary.TryAdd((article.NewsPortalId, article.ArticleId), article.Id);
                _ = savedArticlesTitleDictionary.TryAdd((article.NewsPortalId, article.Title), article.Id);
                _ = savedArticlesSummaryDictionary.TryAdd((article.NewsPortalId, article.Summary), article.Id);
            }

            foreach (var article in articles)
            {
                if (
                    savedArticlesArticleIdDictionary.TryGetValue((article.NewsPortalId, article.ArticleId), out var savedArticleId) ||
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
        #endregion

        #endregion
    }
}


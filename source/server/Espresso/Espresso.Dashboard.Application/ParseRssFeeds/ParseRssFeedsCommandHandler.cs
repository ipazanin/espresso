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
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Espresso.Dashboard.Application.IServices;
using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.ParseRssFeeds
{
    public class ParseRssFeedsCommandHandler : IRequestHandler<ParseRssFeedsCommand, ParseRssFeedsCommandResponse>
    {
        #region Fields
        private readonly ICreateArticleService _parseArticlesService;
        private readonly ILoadRssFeedsService _loadRssFeedsService;
        private readonly ISortArticlesService _sortArticlesService;
        private readonly IGroupSimilarArticlesService _groupSimilarArticlesService;
        private readonly ISendArticlesService _sendArticlesService;
        private readonly IEspressoDatabaseContext _espressoDatabaseContext;
        private readonly ILoggerService<ParseRssFeedsCommandHandler> _loggerService;
        #endregion

        #region Constructors
        public ParseRssFeedsCommandHandler(
            ICreateArticleService parseArticlesService,
            ILoadRssFeedsService loadRssFeedsService,
            ISortArticlesService sortArticlesService,
            IGroupSimilarArticlesService groupSimilarArticlesService,
            ISendArticlesService sendArticlesService,
            IEspressoDatabaseContext espressoDatabaseContext,
            ILoggerService<ParseRssFeedsCommandHandler> loggerService
        )
        {
            _parseArticlesService = parseArticlesService;
            _loadRssFeedsService = loadRssFeedsService;

            _sortArticlesService = sortArticlesService;
            _groupSimilarArticlesService = groupSimilarArticlesService;
            _sendArticlesService = sendArticlesService;
            _espressoDatabaseContext = espressoDatabaseContext;
            _loggerService = loggerService;
        }
        #endregion

        #region Methods
        public async Task<ParseRssFeedsCommandResponse> Handle(
            ParseRssFeedsCommand request,
            CancellationToken cancellationToken
        )
        {
            var rssFeedItems = await _loadRssFeedsService.ParseRssFeeds(
                rssFeeds: request.RssFeeds,
                cancellationToken: cancellationToken
            );

            var articles = await GetArticlesFromLoadedRssFeeds(
                rssFeedItems: rssFeedItems,
                categories: request.Categories,
                cancellationToken: cancellationToken
            );

            var uniqueArticles = _sortArticlesService.RemoveDuplicateArticles(articles);

            var lastSimilarityGroupingTime = request.Articles.Values.Any() ?
                request.Articles.Values.Max(article => article.CreateDateTime) :
                new DateTime();

            var (createArticles, updateArticles, createArticleCategories, deleteArticleCategories) = _sortArticlesService
                .SortArticles(
                    articles: uniqueArticles,
                    savedArticles: request.Articles
                );

            UpdateSavedArticles(
                savedArticles: request.Articles,
                changedArticles: createArticles.Union(updateArticles)
            );

            var similarArticles = _groupSimilarArticlesService.GroupSimilarArticles(
                articles: request.Articles.Values,
                subordinateArticleIds: request.SubordinateArticleIds,
                lastSimilarityGroupingTime: lastSimilarityGroupingTime
            );

            _espressoDatabaseContext.Articles.AddRange(createArticles);
            // NOTE: EF updates all values including attached entities (ArticleCategories)
            _espressoDatabaseContext.Articles.UpdateRange(updateArticles);
            _espressoDatabaseContext.ArticleCategories.AddRange(createArticleCategories);
            _espressoDatabaseContext.ArticleCategories.RemoveRange(deleteArticleCategories);
            _espressoDatabaseContext.SimilarArticles.AddRange(similarArticles);

            await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

            await _sendArticlesService.SendArticlesMessage(
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

        private static void UpdateSavedArticles(
            IDictionary<Guid, Article> savedArticles,
            IEnumerable<Article> changedArticles
        )
        {
            foreach (var changedArticle in changedArticles)
            {
                savedArticles[changedArticle.Id] = changedArticle;
            }
        }

        #endregion
    }
}


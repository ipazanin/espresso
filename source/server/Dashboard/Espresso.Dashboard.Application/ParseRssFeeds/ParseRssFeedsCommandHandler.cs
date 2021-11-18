// ParseRssFeedsCommandHandler.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Dashboard.Application.IServices;
using Espresso.Domain.Entities;
using Espresso.Domain.IServices;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Dashboard.ParseRssFeeds
{
    public class ParseRssFeedsCommandHandler : IRequestHandler<ParseRssFeedsCommand, ParseRssFeedsCommandResponse>
    {
        private readonly ICreateArticleService _parseArticlesService;
        private readonly ILoadRssFeedsService _loadRssFeedsService;
        private readonly ISortArticlesService _sortArticlesService;
        private readonly IGroupSimilarArticlesService _groupSimilarArticlesService;
        private readonly ISendArticlesService _sendArticlesService;
        private readonly IEspressoDatabaseContext _espressoDatabaseContext;
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParseRssFeedsCommandHandler"/> class.
        /// </summary>
        /// <param name="parseArticlesService"></param>
        /// <param name="loadRssFeedsService"></param>
        /// <param name="sortArticlesService"></param>
        /// <param name="groupSimilarArticlesService"></param>
        /// <param name="sendArticlesService"></param>
        /// <param name="espressoDatabaseContext"></param>
        /// <param name="memoryCache"></param>
        public ParseRssFeedsCommandHandler(
            ICreateArticleService parseArticlesService,
            ILoadRssFeedsService loadRssFeedsService,
            ISortArticlesService sortArticlesService,
            IGroupSimilarArticlesService groupSimilarArticlesService,
            ISendArticlesService sendArticlesService,
            IEspressoDatabaseContext espressoDatabaseContext,
            IMemoryCache memoryCache)
        {
            _parseArticlesService = parseArticlesService;
            _loadRssFeedsService = loadRssFeedsService;

            _sortArticlesService = sortArticlesService;
            _groupSimilarArticlesService = groupSimilarArticlesService;
            _sendArticlesService = sendArticlesService;
            _espressoDatabaseContext = espressoDatabaseContext;
            _memoryCache = memoryCache;
        }

        /// <inheritdoc/>
        public async Task<ParseRssFeedsCommandResponse> Handle(
            ParseRssFeedsCommand request,
            CancellationToken cancellationToken)
        {
            _memoryCache.Set(MemoryCacheConstants.DeadLockLogKey, "Before _loadRssFeedsService.ParseRssFeeds");
            var rssFeedItemsChannel = await _loadRssFeedsService.ParseRssFeeds(
                rssFeeds: request.RssFeeds,
                cancellationToken: cancellationToken);

            _memoryCache.Set(MemoryCacheConstants.DeadLockLogKey, "Before _parseArticlesService.CreateArticlesFromRssFeedItems");
            var articlesChannel = await _parseArticlesService.CreateArticlesFromRssFeedItems(
                rssFeedItemChannel: rssFeedItemsChannel,
                categories: request.Categories,
                cancellationToken: cancellationToken);

            _memoryCache.Set(MemoryCacheConstants.DeadLockLogKey, "Before _sortArticlesService.RemoveDuplicateArticles");
            var uniqueArticles = await _sortArticlesService.RemoveDuplicateArticles(articlesChannel, cancellationToken);

            _memoryCache.Set(MemoryCacheConstants.DeadLockLogKey, "Before _sortArticlesService.SortArticles");
            var (createArticles, updateArticlesWithModifiedProperties, createArticleCategories, deleteArticleCategories) = _sortArticlesService
                .SortArticles(
                    articles: uniqueArticles,
                    savedArticles: request.Articles);

            var updateArticles = updateArticlesWithModifiedProperties.Select(updatedArticleWithModifiedproperties => updatedArticleWithModifiedproperties.article);

            _memoryCache.Set(MemoryCacheConstants.DeadLockLogKey, "Before UpdateSavedArticles");
            UpdateSavedArticles(
                savedArticles: request.Articles,
                changedArticles: createArticles.Union(updateArticles));

            var lastSimilarityGroupingTime = request.Articles.Values.Any() ?
                request.Articles.Values.Max(article => article.CreateDateTime) :
                new DateTime();

            _memoryCache.Set(MemoryCacheConstants.DeadLockLogKey, "Before _groupSimilarArticlesService.GroupSimilarArticles");
            var similarArticles = _groupSimilarArticlesService.GroupSimilarArticles(
                articles: request.Articles.Values,
                subordinateArticleIds: request.SubordinateArticleIds,
                lastSimilarityGroupingTime: lastSimilarityGroupingTime);

            _espressoDatabaseContext.Articles.AddRange(createArticles);
            foreach (var (article, modifiedProperties) in updateArticlesWithModifiedProperties)
            {
                foreach (var modifiedProperty in modifiedProperties)
                {
                    _espressoDatabaseContext.Entry(article).Property(modifiedProperty).IsModified = true;
                }
            }

            _espressoDatabaseContext.ArticleCategories.AddRange(createArticleCategories);
            _espressoDatabaseContext.ArticleCategories.RemoveRange(deleteArticleCategories);
            _espressoDatabaseContext.SimilarArticles.AddRange(similarArticles);

            _memoryCache.Set(MemoryCacheConstants.DeadLockLogKey, "Before _espressoDatabaseContext.SaveChangesAsync");
            await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

            _memoryCache.Set(MemoryCacheConstants.DeadLockLogKey, "Before _sendArticlesService.SendArticlesMessage");
            await _sendArticlesService.SendArticlesMessage(
                createArticles: createArticles,
                updateArticles: updateArticles,
                cancellationToken: cancellationToken);

            return new ParseRssFeedsCommandResponse
            {
                CreatedArticles = createArticles.Count(),
                UpdatedArticles = updateArticles.Count(),
            };
        }

        private static void UpdateSavedArticles(
            IDictionary<Guid, Article> savedArticles,
            IEnumerable<Article> changedArticles)
        {
            foreach (var changedArticle in changedArticles)
            {
                savedArticles[changedArticle.Id] = changedArticle;
            }
        }
    }
}

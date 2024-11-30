// ParseRssFeedsCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Dashboard.Application.IServices;
using Espresso.Domain.Entities;
using Espresso.Domain.IServices;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Dashboard.Application.RssFeeds.Commands.ParseRssFeeds;

public class ParseRssFeedsCommandHandler : IRequestHandler<ParseRssFeedsCommand, ParseRssFeedsCommandResponse>
{
    private readonly ICreateArticlesService _parseArticlesService;
    private readonly ILoadRssFeedsService _loadRssFeedsService;
    private readonly ISortArticlesService _sortArticlesService;
    private readonly ISendInformationToApiService _sendArticlesService;
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;
    private readonly IMemoryCache _memoryCache;

    /// <summary>
    /// Initializes a new instance of the <see cref="ParseRssFeedsCommandHandler"/> class.
    /// </summary>
    /// <param name="parseArticlesService"></param>
    /// <param name="loadRssFeedsService"></param>
    /// <param name="sortArticlesService"></param>
    /// <param name="sendArticlesService"></param>
    /// <param name="espressoDatabaseContext"></param>
    /// <param name="memoryCache"></param>
    public ParseRssFeedsCommandHandler(
        ICreateArticlesService parseArticlesService,
        ILoadRssFeedsService loadRssFeedsService,
        ISortArticlesService sortArticlesService,
        ISendInformationToApiService sendArticlesService,
        IEspressoDatabaseContext espressoDatabaseContext,
        IMemoryCache memoryCache)
    {
        _parseArticlesService = parseArticlesService;
        _loadRssFeedsService = loadRssFeedsService;

        _sortArticlesService = sortArticlesService;
        _sendArticlesService = sendArticlesService;
        _espressoDatabaseContext = espressoDatabaseContext;
        _memoryCache = memoryCache;
    }

    /// <inheritdoc/>
    public async Task<ParseRssFeedsCommandResponse> Handle(
        ParseRssFeedsCommand request,
        CancellationToken cancellationToken)
    {
        _ = _memoryCache.Set(MemoryCacheConstants.DeadLockLogKey, "Before _loadRssFeedsService.ParseRssFeeds");

        _ = Task.Run(
            function: async () =>
            {
                await _loadRssFeedsService.ParseRssFeeds(
                    rssFeeds: request.RssFeeds,
                    cancellationToken: cancellationToken);
            },
            cancellationToken: cancellationToken);

        _ = _memoryCache.Set(MemoryCacheConstants.DeadLockLogKey, "Before _parseArticlesService.CreateArticlesFromRssFeedItems");
        _ = Task.Run(
            function: async () =>
            {
                await _parseArticlesService.CreateArticlesFromRssFeedItems(
                            rssFeedItemChannelReader: _loadRssFeedsService.RssFeedItemsChannelReader,
                            categories: request.Categories,
                            cancellationToken: cancellationToken);
            },
            cancellationToken: cancellationToken);

        _ = _memoryCache.Set(MemoryCacheConstants.DeadLockLogKey, "Before _sortArticlesService.RemoveDuplicateArticles");
        var uniqueArticles = await _sortArticlesService.RemoveDuplicateArticles(_parseArticlesService.ArticlesChannelReader, cancellationToken);

        _ = _memoryCache.Set(MemoryCacheConstants.DeadLockLogKey, "Before _sortArticlesService.SortArticles");
        var (createArticles, updateArticlesWithModifiedProperties, createArticleCategories, deleteArticleCategories) = _sortArticlesService
            .SortArticles(
                articles: uniqueArticles,
                savedArticles: request.Articles);

        var updateArticles = updateArticlesWithModifiedProperties
            .Select(updatedArticleWithModifiedProperties => updatedArticleWithModifiedProperties.article);

        _ = _memoryCache.Set(MemoryCacheConstants.DeadLockLogKey, "Before UpdateSavedArticles");
        UpdateSavedArticles(
            savedArticles: request.Articles,
            changedArticles: createArticles.Union(updateArticles));

        _espressoDatabaseContext.Articles.AddRange(createArticles);
        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

        foreach (var (article, modifiedProperties) in updateArticlesWithModifiedProperties)
        {
            foreach (var modifiedProperty in modifiedProperties)
            {
                _espressoDatabaseContext.Entry(article).Property(modifiedProperty).IsModified = true;
            }
        }

        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

        _espressoDatabaseContext.ArticleCategories.AddRange(createArticleCategories);
        _espressoDatabaseContext.ArticleCategories.RemoveRange(deleteArticleCategories);
        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

        _ = _memoryCache.Set(MemoryCacheConstants.DeadLockLogKey, "Before _espressoDatabaseContext.SaveChangesAsync");
        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

        _ = _memoryCache.Set(MemoryCacheConstants.DeadLockLogKey, "Before _sendArticlesService.SendArticlesMessage");
        await _sendArticlesService.SendArticlesMessage(
            createArticleIds: createArticles.Select(article => article.Id),
            updateArticleIds: updateArticles.Select(article => article.Id));

        _ = _memoryCache.Set(MemoryCacheConstants.ArticleKey, request.Articles);

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

// UpdateInMemoryArticlesCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Services.Contracts;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.IServices;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Commands.UpdateInMemoryArticles;

public class UpdateInMemoryArticlesCommandHandler :
    IRequestHandler<UpdateInMemoryArticlesCommand, UpdateInMemoryArticlesCommandResponse>
{
    private readonly IMemoryCache _memoryCache;
    private readonly ITrendingScoreService _trendingScoreService;
    private readonly IRemoveOldArticlesService _removeOldArticlesService;
    private readonly IArticleLoaderService _articleLoaderService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateInMemoryArticlesCommandHandler"/> class.
    /// </summary>
    /// <param name="memoryCache"></param>
    /// <param name="trendingScoreService"></param>
    /// <param name="removeOldArticlesService"></param>
    /// <param name="articleLoaderService"></param>
    public UpdateInMemoryArticlesCommandHandler(
        IMemoryCache memoryCache,
        ITrendingScoreService trendingScoreService,
        IRemoveOldArticlesService removeOldArticlesService,
        IArticleLoaderService articleLoaderService)
    {
        _memoryCache = memoryCache;
        _trendingScoreService = trendingScoreService;
        _removeOldArticlesService = removeOldArticlesService;
        _articleLoaderService = articleLoaderService;
    }

    public async Task<UpdateInMemoryArticlesCommandResponse> Handle(UpdateInMemoryArticlesCommand request, CancellationToken cancellationToken)
    {
        var savedArticles = _memoryCache
            .Get<IList<Article>>(key: MemoryCacheConstants.ArticleKey)!;
        var savedArticlesDictionary = savedArticles.ToDictionary(article => article.Id);

        var newsPortals = _memoryCache
            .Get<IEnumerable<NewsPortal>>(key: MemoryCacheConstants.NewsPortalKey)!;

        var categories = _memoryCache
            .Get<IEnumerable<Category>>(key: MemoryCacheConstants.CategoryKey)!;

        var articleIds = request.CreatedArticleIds.Union(request.UpdatedArticleIds);

        await UpdateInMemoryArticles(
            articlesDictionary: savedArticlesDictionary,
            newsPortals: newsPortals,
            categories: categories,
            articleIds: articleIds,
            cancellationToken: cancellationToken);

        var articles = _removeOldArticlesService
            .RemoveOldArticles(savedArticlesDictionary.Values);

        var articlesToSave = _trendingScoreService.CalculateTrendingScore(articles);

        _memoryCache.Set(
            key: MemoryCacheConstants.ArticleKey,
            value: articlesToSave.ToList());

        var response = new UpdateInMemoryArticlesCommandResponse
        {
            NumberOfUpdatedArticles = request.UpdatedArticleIds.Count(),
            NumberOfCreatedArticles = request.CreatedArticleIds.Count(),
        };

        return response;
    }

    private async Task UpdateInMemoryArticles(
        IDictionary<Guid, Article> articlesDictionary,
        IEnumerable<NewsPortal> newsPortals,
        IEnumerable<Category> categories,
        IEnumerable<Guid> articleIds,
        CancellationToken cancellationToken)
    {
        var loadedArticles = await _articleLoaderService.LoadArticlesForWebApi(
            articleIds: articleIds.ToHashSet(),
            newsPortals: newsPortals,
            categories: categories,
            cancellationToken: cancellationToken);

        _trendingScoreService.CalculateTrendingScore(loadedArticles);

        foreach (var loadedArticle in loadedArticles)
        {
            if (articlesDictionary.TryGetValue(loadedArticle.Id, out var savedArticle))
            {
                articlesDictionary.Remove(savedArticle.Id);
                articlesDictionary.Add(loadedArticle.Id, loadedArticle);
            }
            else
            {
                articlesDictionary.Add(loadedArticle.Id, loadedArticle);
            }
        }
    }
}

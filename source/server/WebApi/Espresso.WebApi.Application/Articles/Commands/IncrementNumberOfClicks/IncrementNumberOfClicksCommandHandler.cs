// IncrementNumberOfClicksCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.IServices;
using Espresso.Persistence.Database;
using Espresso.WebApi.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Commands.IncrementNumberOfClicks;

public class IncrementNumberOfClicksCommandHandler : IRequestHandler<IncrementNumberOfClicksCommand>
{
    private readonly IMemoryCache _memoryCache;
    private readonly IEspressoDatabaseContext _context;
    private readonly ITrendingScoreService _trendingScoreService;

    /// <summary>
    /// Initializes a new instance of the <see cref="IncrementNumberOfClicksCommandHandler"/> class.
    /// </summary>
    /// <param name="memoryCache"></param>
    /// <param name="context"></param>
    /// <param name="trendingScoreService"></param>
    public IncrementNumberOfClicksCommandHandler(
        IMemoryCache memoryCache,
        IEspressoDatabaseContext context,
        ITrendingScoreService trendingScoreService)
    {
        _memoryCache = memoryCache;
        _context = context;
        _trendingScoreService = trendingScoreService;
    }

    public async Task Handle(IncrementNumberOfClicksCommand request, CancellationToken cancellationToken)
    {
        var memoryCacheArticles = _memoryCache
            .Get<IEnumerable<Article>>(key: MemoryCacheConstants.ArticleKey)
            !.ToDictionary(article => article.Id);

        var databaseArticle = await _context.Articles.FindAsync(
            keyValues: new object?[] { request.Id },
            cancellationToken: cancellationToken) ?? throw new NotFoundException(typeName: nameof(Article), id: request.Id.ToString());

        databaseArticle.IncrementNumberOfClicks();
        _context.Articles.Update(databaseArticle);
        await _context.SaveChangesAsync(cancellationToken);

        if (memoryCacheArticles.TryGetValue(request.Id, out var memoryCacheArticle))
        {
            memoryCacheArticle.IncrementNumberOfClicks();

            var articlesWithUpdatedTrendingScore = _trendingScoreService.CalculateTrendingScore(articles: memoryCacheArticles.Values.ToArray());

            _memoryCache.Set(
                key: MemoryCacheConstants.ArticleKey,
                value: articlesWithUpdatedTrendingScore.ToList());
        }
    }
}

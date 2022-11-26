// SetFeaturedArticleCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using Espresso.WebApi.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Commands.SetFeaturedArticle;

public class SetFeaturedArticleCommandHandler : IRequestHandler<SetFeaturedArticleCommand>
{
    private readonly IMemoryCache _memoryCache;
    private readonly IEspressoDatabaseContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="SetFeaturedArticleCommandHandler"/> class.
    /// </summary>
    /// <param name="memoryCache"></param>
    /// <param name="context"></param>
    public SetFeaturedArticleCommandHandler(
        IMemoryCache memoryCache,
        IEspressoDatabaseContext context)
    {
        _memoryCache = memoryCache;
        _context = context;
    }

    public async Task<Unit> Handle(
        SetFeaturedArticleCommand request,
        CancellationToken cancellationToken)
    {
        var memoryCacheArticles = _memoryCache
            .Get<IEnumerable<Article>>(key: MemoryCacheConstants.ArticleKey)
            !.ToDictionary(article => article.Id);

        var articleIds = request.FeaturedArticleConfigurations.Select(featuredArticleConfiguration => featuredArticleConfiguration.articleId);

        var databaseArticles = await _context
            .Articles
            .Where(article => articleIds.Contains(article.Id))
            .ToDictionaryAsync(article => article.Id, cancellationToken);

        foreach (var (articleId, isFeatured, featuredPosition) in request.FeaturedArticleConfigurations)
        {
            if (!databaseArticles.TryGetValue(articleId, out var databaseArticle))
            {
                throw new NotFoundException(
                    typeName: nameof(Article),
                    id: articleId.ToString());
            }

            databaseArticle.SetIsFeaturedValue(isFeatured, featuredPosition);
            _context.Articles.Update(databaseArticle);

            if (memoryCacheArticles.TryGetValue(articleId, out var memoryCacheArticle))
            {
                memoryCacheArticle.SetIsFeaturedValue(isFeatured, featuredPosition);
            }
        }

        await _context.SaveChangesAsync(cancellationToken: default);
        _memoryCache.Set(
            key: MemoryCacheConstants.ArticleKey,
            value: memoryCacheArticles.Values.ToList());

        return Unit.Value;
    }
}

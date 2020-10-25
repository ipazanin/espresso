using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.WebApi.Application.Exceptions;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Commands.HideArticle
{
    public class HideArticleCommandHandler : IRequestHandler<HideArticleCommand>
    {
        private readonly IApplicationDatabaseContext _espressoDatabaseContext;
        private readonly IMemoryCache _memoryCache;

        public HideArticleCommandHandler(
            IApplicationDatabaseContext espressoDatabaseContext,
            IMemoryCache memoryCache
        )
        {
            _espressoDatabaseContext = espressoDatabaseContext;
            _memoryCache = memoryCache;
        }

        public async Task<Unit> Handle(
            HideArticleCommand request,
            CancellationToken cancellationToken
        )
        {
            var memoryCacheArticles = _memoryCache
                .Get<IEnumerable<Article>>(key: MemoryCacheConstants.ArticleKey)
                .ToDictionary(article => article.Id);

            var databaseArticle = await _espressoDatabaseContext.Articles.FindAsync(
                keyValues: new object?[] { request.ArticleId },
                cancellationToken: cancellationToken
            );

            if (databaseArticle is null)
            {
                throw new NotFoundException(
                    typeName: nameof(Article),
                    id: request.ArticleId.ToString()
                );
            }

            databaseArticle.SetIsHidden(request.IsHidden);
            _espressoDatabaseContext.Articles.Update(databaseArticle);
            await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

            if (memoryCacheArticles.TryGetValue(key: request.ArticleId, value: out var memoryCacheArticle))
            {
                memoryCacheArticle.SetIsHidden(request.IsHidden);
                _memoryCache.Set(
                    key: MemoryCacheConstants.ArticleKey,
                    value: memoryCacheArticles.Values.ToList()
                );
            }

            return Unit.Value;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.Exceptions;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.CQRS.Articles.Commands.ToggleFeaturedArticle
{
    public class ToggleFeaturedArticleCommandHandler : IRequestHandler<ToggleFeaturedArticleCommand>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        private readonly IApplicationDatabaseContext _context;
        #endregion

        #region Constructors
        public ToggleFeaturedArticleCommandHandler(
            IMemoryCache memoryCache,
            IApplicationDatabaseContext context
        )
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        #endregion

        #region Methods
        public async Task<Unit> Handle(
            ToggleFeaturedArticleCommand request,
            CancellationToken cancellationToken
        )
        {
            var memoryCacheArticles = _memoryCache
                .Get<IEnumerable<Article>>(key: MemoryCacheConstants.ArticleKey)
                .ToDictionary(article => article.Id);

            var databaseArticle = await _context.Articles.FindAsync(
                keyValues: new object?[] { request.ArticleId },
                cancellationToken: default
            );

            if (databaseArticle != null)
            {
                databaseArticle.ToggleFeatured();
                _ = _context
                    .SaveChangesAsync(cancellationToken: default)
                    ;
            }
            else
            {
                throw new NotFoundException(
                    typeName: nameof(Article),
                    id: request.ArticleId.ToString()
                );
            }

            if (memoryCacheArticles.TryGetValue(request.ArticleId, out var memoryCacheArticle))
            {
                memoryCacheArticle.ToggleFeatured();
                _ = _memoryCache.Set(
                    key: MemoryCacheConstants.ArticleKey,
                    value: memoryCacheArticles.Values.ToList()
                );
            }

            return Unit.Value;
        }
        #endregion
    }
}

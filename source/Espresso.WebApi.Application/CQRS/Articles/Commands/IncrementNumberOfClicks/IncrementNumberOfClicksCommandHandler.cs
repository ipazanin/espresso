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

namespace Espresso.WebApi.Application.CQRS.Articles.Commands.IncrementTrendingArticleScore
{
    public class IncrementNumberOfClicksCommandHandler : IRequestHandler<IncrementNumberOfClicksCommand>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        private readonly IApplicationDatabaseContext _context;
        #endregion

        #region Constructors
        public IncrementNumberOfClicksCommandHandler(
            IMemoryCache memoryCache,
            IApplicationDatabaseContext context
        )
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        #endregion

        #region Methods
        public async Task<Unit> Handle(IncrementNumberOfClicksCommand request, CancellationToken cancellationToken)
        {
            var memoryCacheArticles = _memoryCache
                .Get<IEnumerable<Article>>(key: MemoryCacheConstants.ArticleKey)
                .ToDictionary(article => article.Id);

            var databaseArticle = await _context.Articles.FindAsync(
                keyValues: new object?[] { request.Id },
                cancellationToken: default
            );

            if (databaseArticle != null)
            {
                databaseArticle.IncrementNumberOfClicks();
                _ = _context
                    .SaveChangesAsync(cancellationToken: default)
                    ;
            }
            else
            {
                throw new NotFoundException(typeName: nameof(Article), id: request.Id.ToString());
            }

            if (memoryCacheArticles.TryGetValue(request.Id, out var memoryCacheArticle))
            {
                memoryCacheArticle.IncrementNumberOfClicks();
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

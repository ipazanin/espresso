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
using Microsoft.EntityFrameworkCore;

namespace Espresso.WebApi.Application.Articles.Commands.SetFeaturedArticle
{
    public class SetFeaturedArticleCommandHandler : IRequestHandler<SetFeaturedArticleCommand>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        private readonly IApplicationDatabaseContext _context;
        #endregion

        #region Constructors
        public SetFeaturedArticleCommandHandler(
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
            SetFeaturedArticleCommand request,
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

            if (databaseArticle is null)
            {
                throw new NotFoundException(
                    typeName: nameof(Article),
                    id: request.ArticleId.ToString()
                );
            }

            databaseArticle.SetIsFeaturedValue(request.IsFeatured, request.FeraturedPosition);
            _context.Articles.Update(databaseArticle);
            _ = _context.SaveChangesAsync(cancellationToken: default);

            if (memoryCacheArticles.TryGetValue(request.ArticleId, out var memoryCacheArticle))
            {
                memoryCacheArticle.SetIsFeaturedValue(request.IsFeatured, request.FeraturedPosition);
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

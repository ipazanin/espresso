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
        private readonly IEspressoDatabaseContext _context;
        #endregion

        #region Constructors
        public SetFeaturedArticleCommandHandler(
            IMemoryCache memoryCache,
            IEspressoDatabaseContext context
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
                        id: articleId.ToString()
                    );
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
                value: memoryCacheArticles.Values.ToList()
            );

            return Unit.Value;
        }
        #endregion
    }
}

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
using Espresso.Domain.IServices;

namespace Espresso.WebApi.Application.Articles.Commands.IncrementTrendingArticleScore
{
    public class IncrementNumberOfClicksCommandHandler : IRequestHandler<IncrementNumberOfClicksCommand>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        private readonly IEspressoDatabaseContext _context;
        private readonly ITrendingScoreService _trendingScoreService;
        #endregion

        #region Constructors
        public IncrementNumberOfClicksCommandHandler(
            IMemoryCache memoryCache,
            IEspressoDatabaseContext context,
            ITrendingScoreService trendingScoreService
        )
        {
            _memoryCache = memoryCache;
            _context = context;
            _trendingScoreService = trendingScoreService;
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
                cancellationToken: cancellationToken
            );

            if (databaseArticle is null)
            {
                throw new NotFoundException(typeName: nameof(Article), id: request.Id.ToString());
            }

            databaseArticle.IncrementNumberOfClicks();
            _context.Articles.Update(databaseArticle);
            await _context.SaveChangesAsync(cancellationToken);

            if (memoryCacheArticles.TryGetValue(request.Id, out var memoryCacheArticle))
            {
                memoryCacheArticle.IncrementNumberOfClicks();

                var articlesWithUpdatedTrendingScore = _trendingScoreService.CalculateTrendingScore(articles: memoryCacheArticles.Values);

                _memoryCache.Set(
                    key: MemoryCacheConstants.ArticleKey,
                    value: articlesWithUpdatedTrendingScore.ToList()
                );
            }

            return Unit.Value;
        }
        #endregion
    }
}

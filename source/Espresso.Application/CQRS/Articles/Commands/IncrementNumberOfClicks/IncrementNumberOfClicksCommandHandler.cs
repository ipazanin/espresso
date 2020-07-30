﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Espresso.Application.Exceptions;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;

using MediatR;

using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.CQRS.Articles.Commands.IncrementTrendingArticleScore
{
    public class IncrementNumberOfClicksCommandHandler : IRequestHandler<IncrementNumberOfClicksCommand>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        private readonly IEspressoDatabaseContext _context;
        #endregion

        #region Constructors
        public IncrementNumberOfClicksCommandHandler(
            IMemoryCache memoryCache,
            IEspressoDatabaseContext context
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
                await _context
                    .SaveChangesAsync(cancellationToken: default)
                    .ConfigureAwait(continueOnCapturedContext: false);
            }
            else
            {
                throw new NotFoundException(typeName: nameof(Article), id: request.Id.ToString());
            }

            if (memoryCacheArticles.TryGetValue(request.Id, out var memoryCacheArticle))
            {
                memoryCacheArticle.IncrementNumberOfClicks();
                _memoryCache.Set(
                    key: MemoryCacheConstants.ArticleKey,
                    value: memoryCacheArticles.Values.ToList()
                );
            }

            return Unit.Value;
        }
        #endregion
    }
}
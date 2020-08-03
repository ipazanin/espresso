using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Espresso.Common.Constants;

using Espresso.Domain.Entities;
using Espresso.Persistence.IRepository;
using MediatR;

using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.CQRS.Articles.Commands.DeleteOldArticles
{
    public class DeleteOldArticlesCommandHandler : IRequestHandler<DeleteOldArticlesCommand, DeleteOldArticlesCommandResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        private readonly IArticleRepository _articleRepository;
        #endregion

        #region Constructors
        public DeleteOldArticlesCommandHandler(
            IMemoryCache memoryCache,
            IArticleRepository articleRepository
        )
        {
            _memoryCache = memoryCache;
            _articleRepository = articleRepository;
        }
        #endregion

        #region Methods
        public Task<DeleteOldArticlesCommandResponse> Handle(DeleteOldArticlesCommand request, CancellationToken cancellationToken)
        {
            var maxArticleAge = DateTime.UtcNow - DateTimeConstants.MaxAgeOfArticle;

            var memoryCacheArticles = _memoryCache.Get<IEnumerable<Article>>(key: MemoryCacheConstants.ArticleKey);
            var notOldMemoryCacheArticles = memoryCacheArticles
                .Where(article => article.CreateDateTime > maxArticleAge)
                .ToList();
            _memoryCache.Set(key: MemoryCacheConstants.ArticleKey, value: notOldMemoryCacheArticles);

            var numberOfArticlesToDelete = _articleRepository.DeleteArticles(maxArticleAge);

            var response = new DeleteOldArticlesCommandResponse(numberOfArticlesToDelete);
            return Task.FromResult(response);
        }
        #endregion
    }
}

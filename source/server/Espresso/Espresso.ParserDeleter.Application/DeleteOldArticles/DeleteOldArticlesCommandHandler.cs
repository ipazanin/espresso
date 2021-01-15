using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.IServices;
using Espresso.Persistence.IRepositories;
using MediatR;

using Microsoft.Extensions.Caching.Memory;

namespace Espresso.ParserDeleter.Application.DeleteOldArticles
{
    public class DeleteOldArticlesCommandHandler : IRequestHandler<DeleteOldArticlesCommand, DeleteOldArticlesCommandResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        private readonly IArticleRepository _articleRepository;
        private readonly IRemoveOldArticlesService _removeOldArticlesService;
        #endregion

        #region Constructors
        public DeleteOldArticlesCommandHandler(
            IMemoryCache memoryCache,
            IArticleRepository articleRepository,
            IRemoveOldArticlesService removeOldArticlesService
        )
        {
            _memoryCache = memoryCache;
            _articleRepository = articleRepository;
            _removeOldArticlesService = removeOldArticlesService;
        }
        #endregion

        #region Methods
        public Task<DeleteOldArticlesCommandResponse> Handle(DeleteOldArticlesCommand request, CancellationToken cancellationToken)
        {
            var maxArticleAge = DateTime.UtcNow - request.MaxAgeOfOldArticles;

            var articles = _memoryCache.Get<IDictionary<Guid, Article>>(key: MemoryCacheConstants.ArticleKey);

            var numberOfDeletedMemoryCacheArticles = _removeOldArticlesService.RemoveOldArticlesFromCollection(articles);

            _memoryCache.Set(key: MemoryCacheConstants.ArticleKey, value: articles);

            var numberOfDeletedDatabaseArticles = _articleRepository.DeleteArticlesAndSimilarArticles(maxArticleAge);

            var response = new DeleteOldArticlesCommandResponse
            {
                NumberOfDeletedDatabaseArticles = numberOfDeletedDatabaseArticles,
                NumberOfDeletedMemoryCacheArticles = numberOfDeletedMemoryCacheArticles
            };

            return Task.FromResult(response);
        }
        #endregion
    }
}

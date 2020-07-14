using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Espresso.Application.DataTransferObjects;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;

using MediatR;

using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.CQRS.Articles.Commands.UpdateInMemoryArticles
{
    public class UpdateInMemoryArticlesCommandHandler :
        IRequestHandler<UpdateInMemoryArticlesCommand, UpdateInMemoryArticlesCommandResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Constructors
        public UpdateInMemoryArticlesCommandHandler(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<UpdateInMemoryArticlesCommandResponse> Handle(UpdateInMemoryArticlesCommand request, CancellationToken cancellationToken)
        {
            CreateArticles(request.CreatedArticles);
            UpdateArticles(request.UpdatedArticles);

            RemoveOldArticles();
            var response = new UpdateInMemoryArticlesCommandResponse(request.UpdatedArticles.Count(), request.CreatedArticles.Count());

            return Task.FromResult(result: response);
        }

        private void CreateArticles(IEnumerable<ArticleDto> createArticles)
        {
            var createArticlesDictionary = createArticles
                .Select(ArticleDto.ToArticleProjection)
                .ToDictionary(article => article.Id);

            var savedArticles = _memoryCache
                .Get<IEnumerable<Article>>(MemoryCacheConstants.ArticleKey)
                .ToDictionary(article => article.Id);

            var articlesToSave = savedArticles
                .Union(createArticlesDictionary)
                .Select(articleKeyValuePair => articleKeyValuePair.Value)
                .ToList();

            _memoryCache.Set(
                key: MemoryCacheConstants.ArticleKey,
                value: articlesToSave
            );
        }

        private void UpdateArticles(IEnumerable<ArticleDto> updateArticles)
        {
            var articlesToUpdate = updateArticles
                .Select(ArticleDto.ToArticleProjection);

            var savedArticles = _memoryCache
                .Get<IEnumerable<Article>>(MemoryCacheConstants.ArticleKey)
                .ToDictionary(article => article.Id);

            foreach (var article in articlesToUpdate)
            {
                if (savedArticles.ContainsKey(article.Id))
                {
                    savedArticles.Remove(article.Id);
                    savedArticles.Add(article.Id, article);
                }
            }

            var articlesToSave = savedArticles
                .Select(articleKeyValuePair => articleKeyValuePair.Value)
                .ToList();

            _memoryCache.Set(
                key: MemoryCacheConstants.ArticleKey,
                value: articlesToSave
            );
        }

        private void RemoveOldArticles()
        {
            var maxAge = DateTime.UtcNow - DateTimeConstants.MaxAgeOfArticle;

            var savedArticles = _memoryCache
                .Get<IEnumerable<Article>>(MemoryCacheConstants.ArticleKey);

            var savedArticlesWithoutOldArticles = savedArticles
                .Where(article => article.CreateDateTime > maxAge)
                .ToList();

            _memoryCache.Set(
                key: MemoryCacheConstants.ArticleKey,
                value: savedArticlesWithoutOldArticles
            );
        }
        #endregion
    }
}

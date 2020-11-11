using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.IServices;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Commands.UpdateInMemoryArticles
{
    public class UpdateInMemoryArticlesCommandHandler :
        IRequestHandler<UpdateInMemoryArticlesCommand, UpdateInMemoryArticlesCommandResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        private readonly IApplicationDatabaseContext _applicationDatabaseContext;
        private readonly ITrendingScoreService _trendingScoreService;
        #endregion

        #region Constructors
        public UpdateInMemoryArticlesCommandHandler(
            IMemoryCache memoryCache,
            IApplicationDatabaseContext applicationDatabaseContext,
            ITrendingScoreService trendingScoreService
        )
        {
            _memoryCache = memoryCache;
            _applicationDatabaseContext = applicationDatabaseContext;
            _trendingScoreService = trendingScoreService;
        }
        #endregion

        #region Methods
        public async Task<UpdateInMemoryArticlesCommandResponse> Handle(UpdateInMemoryArticlesCommand request, CancellationToken cancellationToken)
        {
            var savedArticlesDictionary = _memoryCache
                .Get<IEnumerable<Article>>(key: MemoryCacheConstants.ArticleKey)
                .ToDictionary(article => article.Id);

            await CreateArticles(savedArticlesDictionary, request.CreatedArticleIds, cancellationToken);
            await UpdateArticles(savedArticlesDictionary, request.UpdatedArticleIds, cancellationToken);

            RemoveOldArticles(savedArticlesDictionary, request.MaxAgeOfArticle);

            var articles = savedArticlesDictionary
                .Select(articleKeyValuePair => articleKeyValuePair.Value)
                .ToList();

            var articlesToSave = _trendingScoreService.CalculateTrendingScore(articles);

            _memoryCache.Set(
                key: MemoryCacheConstants.ArticleKey,
                value: articlesToSave
            );

            var response = new UpdateInMemoryArticlesCommandResponse
            {
                NumberOfUpdatedArticles = request.CreatedArticleIds.Count(),
                NumberOfCreatedArticles = request.UpdatedArticleIds.Count()
            };

            return response;
        }

        private async Task CreateArticles(
            IDictionary<Guid, Article> savedArticlesDictionary,
            IEnumerable<Guid> createdArticleIds,
            CancellationToken cancellationToken
        )
        {

            var articlesToCreate = await _applicationDatabaseContext
                .Articles
                .Include(article => article.ArticleCategories)
                .ThenInclude(articleCategory => articleCategory.Category)
                .Include(article => article.NewsPortal)
                .Include(article => article.MainArticle)
                .ThenInclude(mainArticle => mainArticle!.MainArticle)
                .Include(article => article.SubordinateArticles)
                .ThenInclude(subordinateArticle => subordinateArticle!.SubordinateArticle)
                .ThenInclude(article => article!.NewsPortal)
                .Include(article => article.SubordinateArticles)
                .ThenInclude(subordinateArticle => subordinateArticle!.SubordinateArticle)
                .ThenInclude(article => article!.ArticleCategories)
                .ThenInclude(articleCategory => articleCategory.Category)
                .Where(article => createdArticleIds.Contains(article.Id))
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            foreach (var article in articlesToCreate)
            {
                if (savedArticlesDictionary.ContainsKey(article.Id))
                {
                    _ = savedArticlesDictionary.Remove(article.Id);
                    savedArticlesDictionary.Add(article.Id, article);
                }
                else
                {
                    savedArticlesDictionary.Add(article.Id, article);
                }
            }
        }

        private async Task UpdateArticles(
            IDictionary<Guid, Article> savedArticlesDictionary,
            IEnumerable<Guid> updatedArticleIds,
            CancellationToken cancellationToken
        )
        {
            var articlesToUpdate = await _applicationDatabaseContext
                .Articles
                .Include(article => article.ArticleCategories)
                .ThenInclude(articleCategory => articleCategory.Category)
                .Include(article => article.NewsPortal)
                .Include(article => article.MainArticle)
                .ThenInclude(mainArticle => mainArticle!.MainArticle)
                .Include(article => article.SubordinateArticles)
                .ThenInclude(subordinateArticle => subordinateArticle!.SubordinateArticle)
                .ThenInclude(article => article!.NewsPortal)
                .Include(article => article.SubordinateArticles)
                .ThenInclude(subordinateArticle => subordinateArticle!.SubordinateArticle)
                .ThenInclude(article => article!.ArticleCategories)
                .ThenInclude(articleCategory => articleCategory.Category)
                .Where(article => updatedArticleIds.Contains(article.Id))
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            foreach (var article in articlesToUpdate)
            {
                if (savedArticlesDictionary.ContainsKey(article.Id))
                {
                    savedArticlesDictionary.Remove(article.Id);
                    savedArticlesDictionary.Add(article.Id, article);
                }
                else
                {
                    savedArticlesDictionary.Add(article.Id, article);
                }
            }
        }

        private void RemoveOldArticles(
            IDictionary<Guid, Article> savedArticlesDictionary,
            TimeSpan maxAgeOfArticle
        )
        {
            var maxAge = DateTime.UtcNow - maxAgeOfArticle;

            var articlesToRemove = savedArticlesDictionary
                .Values
                .Where(article => article.CreateDateTime < maxAge)
                .ToList();

            foreach (var articleToRemove in articlesToRemove)
            {
                savedArticlesDictionary.Remove(articleToRemove.Id);
            }

            _memoryCache.Set(
                key: MemoryCacheConstants.ArticleKey,
                value: articlesToRemove
            );
        }
        #endregion
    }
}

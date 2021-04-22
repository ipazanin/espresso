using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.DataTransferObjects.ArticleDataTransferObjects;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.IServices;
using Espresso.Domain.ValueObjects.ArticleValueObjects;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Commands.UpdateInMemoryArticles
{
    public class UpdateInMemoryArticlesCommandHandler :
        IRequestHandler<UpdateInMemoryArticlesCommand, UpdateInMemoryArticlesCommandResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        private readonly ITrendingScoreService _trendingScoreService;
        private readonly IRemoveOldArticlesService _removeOldArticlesService;
        #endregion

        #region Constructors
        public UpdateInMemoryArticlesCommandHandler(
            IMemoryCache memoryCache,
            ITrendingScoreService trendingScoreService,
            IRemoveOldArticlesService removeOldArticlesService
        )
        {
            _memoryCache = memoryCache;
            _trendingScoreService = trendingScoreService;
            _removeOldArticlesService = removeOldArticlesService;
        }
        #endregion

        #region Methods
        public Task<UpdateInMemoryArticlesCommandResponse> Handle(UpdateInMemoryArticlesCommand request, CancellationToken cancellationToken)
        {
            var savedArticlesDictionary = _memoryCache
                .Get<IEnumerable<Article>>(key: MemoryCacheConstants.ArticleKey)
                .ToDictionary(article => article.Id);

            var newsPortalsDictionary = _memoryCache
                .Get<IEnumerable<NewsPortal>>(key: MemoryCacheConstants.NewsPortalKey)
                .ToDictionary(newsPortal => newsPortal.Id);

            var categoriesDictionary = _memoryCache
                .Get<IEnumerable<Category>>(key: MemoryCacheConstants.CategoryKey)
                .ToDictionary(category => category.Id);


            var articleDtos = request.CreatedArticles.Union(request.UpdatedArticles);

            UpdateInMemoryArticles(
                articlesDictionary: savedArticlesDictionary,
                newsPortalsDictionary: newsPortalsDictionary,
                categoriesDictionary: categoriesDictionary,
                articleDtos: articleDtos
            );

            var articles = _removeOldArticlesService
                .RemoveOldArticles(savedArticlesDictionary.Values);

            var articlesToSave = _trendingScoreService.CalculateTrendingScore(articles);

            _memoryCache.Set(
                key: MemoryCacheConstants.ArticleKey,
                value: articlesToSave.ToList()
            );

            var response = new UpdateInMemoryArticlesCommandResponse
            {
                NumberOfUpdatedArticles = request.UpdatedArticles.Count(),
                NumberOfCreatedArticles = request.CreatedArticles.Count()
            };

            return Task.FromResult(response);
        }

        private static void UpdateInMemoryArticles(
            IDictionary<Guid, Article> articlesDictionary,
            IDictionary<int, NewsPortal> newsPortalsDictionary,
            IDictionary<int, Category> categoriesDictionary,
            IEnumerable<ArticleDto> articleDtos
        )
        {
            foreach (var articleDto in articleDtos)
            {
                var article = CreateArticle(
                    articleDto: articleDto,
                    newsPortalsDictionary: newsPortalsDictionary,
                    categoriesDictionary: categoriesDictionary
                );

                if (articlesDictionary.ContainsKey(article.Id))
                {
                    articlesDictionary.Remove(article.Id);
                    articlesDictionary.Add(article.Id, article);
                }
                else
                {
                    articlesDictionary.Add(article.Id, article);
                }
            }

            var articleDtoIds = articleDtos.Select(articleDto => articleDto.Id);
            var newArticles = articlesDictionary
                .Values
                .Where(article => articleDtoIds.Contains(article.Id));

            foreach (var article in newArticles)
            {
                if (
                    article.MainArticle is not null &&
                    articlesDictionary.TryGetValue(article.MainArticle.MainArticleId, out var mainArticle)
                )
                {
                    var subordinateArticle = article;

                    subordinateArticle.MainArticle.SetMainArticle(mainArticle);
                    subordinateArticle.MainArticle.SetSubordinateArticle(subordinateArticle);

                    mainArticle.SubordinateArticles.Add(article.MainArticle);
                }
            }
        }

        private static Article CreateArticle(
            ArticleDto articleDto,
            IDictionary<int, NewsPortal> newsPortalsDictionary,
            IDictionary<int, Category> categoriesDictionary
        )
        {
            var articleCategories = articleDto
                .ArticleCategories
                .Select(articleCategory => new ArticleCategory(
                    id: articleCategory.Id,
                    articleId: articleDto.Id,
                    categoryId: articleCategory.CategoryId,
                    article: null,
                    category: categoriesDictionary[articleCategory.CategoryId]
                ));

            var mainArticle = articleDto.MainArticle == null ?
                null :
                new SimilarArticle(
                    id: articleDto.MainArticle.Id,
                    similarityScore: articleDto.MainArticle.SimilarityScore,
                    mainArticleId: articleDto.MainArticle.MainArticleId,
                    mainArticle: null,
                    subordinateArticleId: articleDto.MainArticle.SubordinateArticleId,
                    subordinateArticle: null
                );

            var article = new Article(
                id: articleDto.Id,
                url: articleDto.Url,
                webUrl: articleDto.WebUrl,
                summary: articleDto.Summary,
                title: articleDto.Title,
                imageUrl: articleDto.ImageUrl,
                createDateTime: articleDto.CreateDateTime,
                updateDateTime: articleDto.UpdateDateTime,
                publishDateTime: articleDto.PublishDateTime!,
                numberOfClicks: articleDto.NumberOfClicks,
                trendingScore: articleDto.TrendingScore,
                editorConfiguration: new EditorConfiguration(),
                newsPortalId: articleDto.NewsPortalId,
                rssFeedId: articleDto.RssFeedId,
                articleCategories: articleCategories,
                newsPortal: newsPortalsDictionary[articleDto.NewsPortalId],
                rssFeed: null,
                subordinateArticles: null,
                mainArticle: mainArticle
            );

            return article;
        }
        #endregion
    }
}

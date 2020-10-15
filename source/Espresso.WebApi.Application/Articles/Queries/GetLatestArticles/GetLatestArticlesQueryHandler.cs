using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles
{
    public class GetArticlesQueryHandler : IRequestHandler<GetLatestArticlesQuery, GetLatestArticlesQueryResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Constructors
        public GetArticlesQueryHandler(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<GetLatestArticlesQueryResponse> Handle(
            GetLatestArticlesQuery request,
            CancellationToken cancellationToken
        )
        {
            var articles = GetLatestArticles(
                request: request
            );

            var newNewsPortals = GetNewNewsPortals(
                request: request
            );

            var featuredArticles = GetFeaturedArticles(
                request: request
            );

            var response = new GetLatestArticlesQueryResponse
            {
                Articles = articles,
                FeaturedArticles = featuredArticles,
                NewNewsPortals = newNewsPortals,
                NewNewsPortalsPosition = request.NewNewsPortalsPosition
            };

            return Task.FromResult(result: response);
        }

        private IEnumerable<GetLatestArticlesArticle> GetLatestArticles(
            GetLatestArticlesQuery request
        )
        {
            var (newsPortalIds, categoryIds) = ParseIds(request);

            var articles = _memoryCache.Get<IEnumerable<Article>>(
                key: MemoryCacheConstants.ArticleKey
            );

            var firstArticle = articles.FirstOrDefault(
                article => article.Id.Equals(request.FirstArticleId)
            );

            var articleDtos = articles
                .OrderByDescending(keySelector: Article.GetOrderByDescendingPublishDateExpression().Compile())
                .Where(
                    predicate: Article.GetFilteredArticlesPredicate(
                        categoryIds: categoryIds,
                        newsPortalIds: newsPortalIds,
                        titleSearchQuery: request.TitleSearchQuery,
                        articleCreateDateTime: firstArticle?.CreateDateTime
                    ).Compile()
                )
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(GetLatestArticlesArticle.GetProjection().Compile());

            return articleDtos;
        }

        private IEnumerable<GetLatestArticlesArticle> GetFeaturedArticles(
            GetLatestArticlesQuery request
        )
        {
            var articles = _memoryCache.Get<IEnumerable<Article>>(
                key: MemoryCacheConstants.ArticleKey
            );

            var featuredArticles = articles
                .Where(
                    Article.GetFilteredFeaturedArticlesPredicate(
                        categoryIds: null,
                        newsPortalIds: null,
                        titleSearchQuery: null,
                        maxAgeOfFeaturedArticle: request.MaxAgeOfFeaturedArticle,
                        articleCreateDateTime: null
                    )
                    .Compile()
                )
                .OrderByDescending(Article.GetOrderByFeaturedArticlesExpression().Compile())
                .ThenByDescending(Article.GetOrderByDescendingTrendingScoreExpression().Compile());

            var trendingArticles = articles
                .Where(
                    Article.GetTrendingArticlePredicate(
                        maxAgeOfTrendingArticle: request.MaxAgeOfTrendingArticle,
                        articleCreateDateTime: null
                    )
                    .Compile()
                )
                .OrderByDescending(Article.GetOrderByDescendingTrendingScoreExpression().Compile());

            var articleDtos = featuredArticles
                .Union(trendingArticles)
                .Take(request.Take)
                .Select(GetLatestArticlesArticle.GetProjection().Compile());

            return articleDtos;
        }

        private IEnumerable<GetLatestArticlesNewsPortal> GetNewNewsPortals(
            GetLatestArticlesQuery request
        )
        {
            var (newsPortalIds, categoryIds) = ParseIds(request);
            var newsPortals = _memoryCache.Get<IEnumerable<NewsPortal>>(
                key: MemoryCacheConstants.NewsPortalKey
            );

            var newsPortalDtos = newsPortals
                .Where(
                    predicate: NewsPortal.GetLatestSugestedNewsPortalsPredicate(
                        newsPortalIds: newsPortalIds,
                        categoryIds: categoryIds,
                        maxAgeOfNewNewsPortal: request.MaxAgeOfNewNewsPortal
                    ).Compile()
                )
                .Select(selector: GetLatestArticlesNewsPortal.GetProjection().Compile());

            var random = new Random();

            return newsPortalDtos.OrderBy(newsPortal => random.Next());
        }

        private static (IEnumerable<int>? newsPortalIds, IEnumerable<int>? categoryIds) ParseIds(GetLatestArticlesQuery request)
        {
            var newsPortalIds = request.NewsPortalIds
                ?.Replace(" ", "")
                ?.Split(',')
                ?.Select(newsPortalIdString => int.TryParse(newsPortalIdString, out var newsPortalId) ? newsPortalId : default)
                ?.Where(newsPortalId => newsPortalId != default);

            var categoryIds = request.CategoryIds
                ?.Replace(" ", "")
                ?.Split(',')
                ?.Select(categoryIdString => int.TryParse(categoryIdString, out var categoryId) ? categoryId : default)
                ?.Where(categoryId => categoryId != default);

            return (newsPortalIds, categoryIds);
        }

        #endregion
    }
}

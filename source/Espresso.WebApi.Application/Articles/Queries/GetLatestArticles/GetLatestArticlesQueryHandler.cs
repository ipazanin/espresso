using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Common.Extensions;
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
            var (newsPortalIds, categoryIds) = ParseIds(request);

            var savedArticles = _memoryCache.Get<IEnumerable<Article>>(
                key: MemoryCacheConstants.ArticleKey
            );

            var firstArticle = savedArticles.FirstOrDefault(
                article => article.Id.Equals(request.FirstArticleId)
            );

            var newsPortals = _memoryCache.Get<IEnumerable<NewsPortal>>(
                key: MemoryCacheConstants.NewsPortalKey
            );

            var articles = GetLatestArticles(
                savedArticles: savedArticles,
                firstArticleCreateDateTime: firstArticle?.CreateDateTime,
                request: request,
                categoryIds: categoryIds,
                newsPortalIds: newsPortalIds
            );

            var newNewsPortals = GetNewNewsPortals(
                newsPortals: newsPortals,
                request: request,
                categoryIds: categoryIds,
                newsPortalIds: newsPortalIds
            );

            var featuredArticles = GetFeaturedArticles(
                savedArticles: savedArticles,
                firstArticleCreateDateTime: firstArticle?.CreateDateTime,
                request: request,
                categoryIds: categoryIds
            );

            var response = new GetLatestArticlesQueryResponse
            {
                Articles = articles,
                FeaturedArticles = featuredArticles,
                NewNewsPortals = newNewsPortals,
                NewNewsPortalsPosition = request.NewNewsPortalsPosition,
                LastAddedNewsPortalDate = newsPortals
                    .OrderByDescending(newsPortal => newsPortal.CreatedAt)
                    .First()
                    .CreatedAt
                    .ToString(DateTimeConstants.MobileAppDateTimeFormat)
            };

            return Task.FromResult(result: response);
        }

        private static IEnumerable<IEnumerable<GetLatestArticlesArticle>> GetLatestArticles(
            IEnumerable<Article> savedArticles,
            DateTime? firstArticleCreateDateTime,
            GetLatestArticlesQuery request,
            IEnumerable<int>? categoryIds,
            IEnumerable<int>? newsPortalIds
        )
        {

            var articles = savedArticles
                .OrderArticlesByPublishDate()
                .Where(
                    predicate: Article.GetFilteredLatestArticlesPredicate(
                        categoryIds: categoryIds,
                        newsPortalIds: newsPortalIds,
                        titleSearchTerm: request.TitleSearchQuery,
                        articleCreateDateTime: firstArticleCreateDateTime
                    ).Compile()
                )
                .Skip(request.Skip)
                .Take(request.Take);


            var projection = GetLatestArticlesArticle.GetProjection().Compile();
            var articleDtos = articles
                .Select(article => new List<GetLatestArticlesArticle>()
                    {
                        projection.Invoke(article)
                    }.Union(article.SubordinateArticles.Select(similarArticle => projection.Invoke(similarArticle.SubordinateArticle!)))
                );

            return articleDtos;
        }

        private static IEnumerable<GetLatestArticlesArticle> GetFeaturedArticles(
            IEnumerable<Article> savedArticles,
            DateTime? firstArticleCreateDateTime,
            GetLatestArticlesQuery request,
            IEnumerable<int>? categoryIds
        )
        {
            if (request.Skip != 0)
            {
                return Array.Empty<GetLatestArticlesArticle>();
            }

            var featuredArticles = savedArticles
                .Where(
                    Article.GetFilteredFeaturedArticlesPredicate(
                        categoryIds: null,
                        newsPortalIds: null,
                        maxAgeOfFeaturedArticle: request.MaxAgeOfFeaturedArticle,
                        articleCreateDateTime: firstArticleCreateDateTime
                    )
                    .Compile()
                )
                .OrderFeaturedArticles(categoryIds);

            var trendingArticlesTake = request.FeaturedArticlesTake - featuredArticles.Count() <= 0 ?
                0 :
                request.FeaturedArticlesTake - featuredArticles.Count();

            var trendingArticles = savedArticles
                .Where(
                    Article.GetTrendingArticlePredicate(
                        maxAgeOfTrendingArticle: request.MaxAgeOfTrendingArticle,
                        articleCreateDateTime: null
                    )
                    .Compile()
                )
                .OrderArticlesByTrendingScore()
                .Take(trendingArticlesTake)
                .OrderArticlesByCategory(categoryIds);

            var joinedArticles = featuredArticles
                .Union(trendingArticles);

            var articleDtos = joinedArticles
                .Take(request.FeaturedArticlesTake)
                .Select(GetLatestArticlesArticle.GetProjection().Compile());

            return articleDtos;
        }

        private static IEnumerable<GetLatestArticlesNewsPortal> GetNewNewsPortals(
            IEnumerable<NewsPortal> newsPortals,
            IEnumerable<int>? newsPortalIds,
            IEnumerable<int>? categoryIds,
            GetLatestArticlesQuery request
        )
        {
            if (request.Skip != 0)
            {
                return Array.Empty<GetLatestArticlesNewsPortal>();
            }

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

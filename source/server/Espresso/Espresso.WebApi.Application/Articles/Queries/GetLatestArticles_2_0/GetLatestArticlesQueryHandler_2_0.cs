using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Common.Extensions;
using Espresso.Domain.Entities;
using Espresso.Domain.Extensions;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_2_0
{
    public class GetArticlesQueryHandler_2_0 : IRequestHandler<GetLatestArticlesQuery_2_0, GetLatestArticlesQueryResponse_2_0>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Constructors
        public GetArticlesQueryHandler_2_0(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<GetLatestArticlesQueryResponse_2_0> Handle(
            GetLatestArticlesQuery_2_0 request,
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
                request: request
            );

            var featuredArticles = GetFeaturedArticles(
                savedArticles: savedArticles,
                firstArticleCreateDateTime: firstArticle?.CreateDateTime,
                request: request,
                categoryIds: categoryIds
            );

            var response = new GetLatestArticlesQueryResponse_2_0
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

        private static IEnumerable<GetLatestArticlesArticle_2_0> GetLatestArticles(
            IEnumerable<Article> savedArticles,
            DateTime? firstArticleCreateDateTime,
            GetLatestArticlesQuery_2_0 request,
            IEnumerable<int>? categoryIds,
            IEnumerable<int>? newsPortalIds
        )
        {

            var articles = savedArticles
                .OrderArticlesByPublishDate()
                .FilterArticles_2_0(
                    categoryIds: categoryIds,
                    newsPortalIds: newsPortalIds,
                    titleSearchTerm: request.TitleSearchQuery,
                    articleCreateDateTime: firstArticleCreateDateTime
                )
                // .FilterArticlesWithCoronaVirusContentForIosRelease(request.DeviceType, request.TargetedApiVersion)
                .Skip(request.Skip)
                .Take(request.Take);

            var articleDtos = articles
                .Select(GetLatestArticlesArticle_2_0.GetProjection().Compile());

            return articleDtos;
        }

        private static IEnumerable<GetLatestArticlesArticle_2_0> GetFeaturedArticles(
            IEnumerable<Article> savedArticles,
            DateTime? firstArticleCreateDateTime,
            GetLatestArticlesQuery_2_0 request,
            IEnumerable<int>? categoryIds
        )
        {
            if (request.Skip != 0)
            {
                return Array.Empty<GetLatestArticlesArticle_2_0>();
            }

            var featuredArticles = savedArticles
                .FilterFeaturedArticles(
                    categoryIds: null,
                    newsPortalIds: null,
                    maxAgeOfFeaturedArticle: request.MaxAgeOfFeaturedArticle,
                    articleCreateDateTime: firstArticleCreateDateTime
                )
                // .FilterArticlesWithCoronaVirusContentForIosRelease(request.DeviceType, request.TargetedApiVersion)
                .OrderFeaturedArticles(categoryIds);

            var trendingArticlesTake = request.FeaturedArticlesTake - featuredArticles.Count() <= 0 ?
                0 :
                request.FeaturedArticlesTake - featuredArticles.Count();

            var trendingArticles = savedArticles
                .FilterTrendingArticles(
                    maxAgeOfTrendingArticle: request.MaxAgeOfTrendingArticle,
                    articleCreateDateTime: null
                )
                // .FilterArticlesWithCoronaVirusContentForIosRelease(request.DeviceType, request.TargetedApiVersion)
                .OrderArticlesByTrendingScore()
                .Take(trendingArticlesTake)
                .OrderArticlesByCategory(categoryIds);

            var articleDtos = featuredArticles
                .Union(trendingArticles)
                .Take(request.FeaturedArticlesTake)
                .Select(GetLatestArticlesArticle_2_0.GetProjection().Compile());

            return articleDtos;
        }

        private static IEnumerable<GetLatestArticlesNewsPortal_2_0> GetNewNewsPortals(
            IEnumerable<NewsPortal> newsPortals,
            GetLatestArticlesQuery_2_0 request
        )
        {
            if (request.Skip != 0)
            {
                return Array.Empty<GetLatestArticlesNewsPortal_2_0>();
            }

            var (newsPortalIds, categoryIds) = ParseIds(request);

            var newsPortalDtos = newsPortals
                .Where(
                    predicate: NewsPortal.GetLatestSugestedNewsPortalsPredicate(
                        newsPortalIds: newsPortalIds,
                        categoryIds: categoryIds,
                        maxAgeOfNewNewsPortal: request.MaxAgeOfNewNewsPortal
                    ).Compile()
                )
                .Select(selector: GetLatestArticlesNewsPortal_2_0.GetProjection().Compile());

            var random = new Random();

            return newsPortalDtos.OrderBy(newsPortal => random.Next());
        }

        private static (IEnumerable<int>? newsPortalIds, IEnumerable<int>? categoryIds) ParseIds(GetLatestArticlesQuery_2_0 request)
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

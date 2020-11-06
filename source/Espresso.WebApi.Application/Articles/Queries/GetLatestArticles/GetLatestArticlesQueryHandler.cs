using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
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
                request: request
            );

            var newNewsPortals = GetNewNewsPortals(
                newsPortals: newsPortals,
                request: request
            );

            var featuredArticles = GetFeaturedArticles(
                savedArticles: savedArticles,
                firstArticleCreateDateTime: firstArticle?.CreateDateTime,
                request: request
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
            GetLatestArticlesQuery request
        )
        {
            var (newsPortalIds, categoryIds) = ParseIds(request);

            var articles = savedArticles
                .OrderByDescending(keySelector: Article.GetOrderByDescendingPublishDateExpression().Compile())
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

            var filteredArticles = FilterArticlesWithCoronaVirusContentForIosRelease(articles, request);

            var projection = GetLatestArticlesArticle.GetProjection().Compile();
            var articleDtos = filteredArticles
                .Select(article => new List<GetLatestArticlesArticle>()
                {
                    projection.Invoke(article)
                }.Union(article.SubordinateArticles.Select(similarArticle => projection.Invoke(similarArticle.SubordinateArticle!))));

            return articleDtos;
        }

        private static IEnumerable<GetLatestArticlesArticle> GetFeaturedArticles(
            IEnumerable<Article> savedArticles,
            DateTime? firstArticleCreateDateTime,
            GetLatestArticlesQuery request
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
                .OrderBy(Article.GetOrderByFeaturedArticlesExpression().Compile())
                .ThenByDescending(Article.GetOrderByDescendingTrendingScoreExpression().Compile());

            var trendingArticles = savedArticles
                .Where(
                    Article.GetTrendingArticlePredicate(
                        maxAgeOfTrendingArticle: request.MaxAgeOfTrendingArticle,
                        articleCreateDateTime: null
                    )
                    .Compile()
                )
                .OrderByDescending(Article.GetOrderByDescendingTrendingScoreExpression().Compile());

            var joinedArticles = featuredArticles
                .Union(trendingArticles)
                .ToList();

            var filteredArticles = FilterArticlesWithCoronaVirusContentForIosRelease(joinedArticles, request).ToList();

            var articleDtos = filteredArticles
                .Take(request.Take)
                .Select(GetLatestArticlesArticle.GetProjection().Compile());

            return articleDtos;
        }

        private static IEnumerable<GetLatestArticlesNewsPortal> GetNewNewsPortals(
            IEnumerable<NewsPortal> newsPortals,
            GetLatestArticlesQuery request
        )
        {
            if (request.Skip != 0)
            {
                return Array.Empty<GetLatestArticlesNewsPortal>();
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

        private static IEnumerable<Article> FilterArticlesWithCoronaVirusContentForIosRelease(
            IEnumerable<Article> articles,
            GetLatestArticlesQuery request
        )
        {
            if (
                !(
                    request.DeviceType == DeviceType.Ios &&
                    request.TargetedApiVersion == "2.0"
                )
            )
            {
                return articles;
            }

            return articles.Where(
                article => !DefaultValueConstants.BannedKeywords.Any(
                    bannedKeyword => article.Title.Contains(bannedKeyword, StringComparison.InvariantCultureIgnoreCase) ||
                        article.Summary.Contains(bannedKeyword, StringComparison.InvariantCultureIgnoreCase)
                )
            );
        }
        #endregion
    }
}

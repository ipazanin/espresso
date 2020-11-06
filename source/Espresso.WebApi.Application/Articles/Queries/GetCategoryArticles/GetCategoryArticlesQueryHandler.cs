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

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles
{
    public class GetCategoryArticlesQueryHandler : IRequestHandler<GetCategoryArticlesQuery, GetCategoryArticlesQueryResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Contructors
        public GetCategoryArticlesQueryHandler(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<GetCategoryArticlesQueryResponse> Handle(
            GetCategoryArticlesQuery request,
            CancellationToken cancellationToken
        )
        {
            var articles = _memoryCache.Get<IEnumerable<Article>>(
                key: MemoryCacheConstants.ArticleKey
            );

            var firstArticle = articles.FirstOrDefault(
                article => article.Id.Equals(request.FirstArticleId)
            );

            var newsPortalIds = request.NewsPortalIds
                ?.Replace(" ", "")
                ?.Split(',')
                ?.Select(newsPortalIdString => int.TryParse(newsPortalIdString, out var newsPortalId) ? newsPortalId : default)
                ?.Where(newsPortalId => newsPortalId != default);

            var filteredArticles = articles
                .OrderByDescending(keySelector: Article.GetOrderByDescendingPublishDateExpression().Compile())
                .Where(
                    predicate: Article.GetFilteredCategoryArticlesPredicate(
                        categoryId: request.CategoryId,
                        newsPortalIds: newsPortalIds,
                        searchTerm: request.TitleSearchQuery,
                        articleCreateDateTime: firstArticle?.CreateDateTime
                    ).Compile()
                );


            var coronaFilteredArticles = FilterArticlesWithCoronaVirusContentForIosRelease(filteredArticles, request)
                .Skip(request.Skip)
                .Take(request.Take);

            var articleDtos = coronaFilteredArticles.Select(GetCategoryArticlesArticle.GetProjection().Compile());

            var newsPortalDtos = GetNewNewsPortals(
                newsPortalIds: newsPortalIds,
                request: request
            );

            var random = new Random();

            var response = new GetCategoryArticlesQueryResponse
            {
                Articles = articleDtos,
                NewNewsPortals = newsPortalDtos.OrderBy(newsPortal => random.Next()),
                NewNewsPortalsPosition = request.NewNewsPortalsPosition
            };

            return Task.FromResult(result: response);
        }

        private IEnumerable<GetCategoryArticlesNewsPortal> GetNewNewsPortals(
            IEnumerable<int>? newsPortalIds,
            GetCategoryArticlesQuery request
        )
        {
            if (request.Skip != 0)
            {
                return Array.Empty<GetCategoryArticlesNewsPortal>();
            }

            var newsPortals = _memoryCache.Get<IEnumerable<NewsPortal>>(
                key: MemoryCacheConstants.NewsPortalKey
            );

            var newsPortalDtos = newsPortals
                .Where(
                    NewsPortal.GetCategorySugestedNewsPortalsPredicate(
                        newsPortalIds: newsPortalIds,
                        categoryId: request.CategoryId,
                        regionId: request.RegionId,
                        maxAgeOfNewNewsPortal: request.MaxAgeOfNewNewsPortal
                    )
                    .Compile()
                )
                .Select(selector: GetCategoryArticlesNewsPortal.GetProjection().Compile());

            return newsPortalDtos;
        }

        private static IEnumerable<Article> FilterArticlesWithCoronaVirusContentForIosRelease(
            IEnumerable<Article> articles,
            GetCategoryArticlesQuery request
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

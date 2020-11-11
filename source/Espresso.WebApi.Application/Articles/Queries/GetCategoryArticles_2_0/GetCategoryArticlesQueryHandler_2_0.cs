using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Common.Extensions;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_2_0
{
    public class GetCategoryArticlesQueryHandler_2_0 : IRequestHandler<GetCategoryArticlesQuery_2_0, GetCategoryArticlesQueryResponse_2_0>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Contructors
        public GetCategoryArticlesQueryHandler_2_0(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<GetCategoryArticlesQueryResponse_2_0> Handle(
            GetCategoryArticlesQuery_2_0 request,
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
                .OrderArticlesByPublishDate()
                .Where(
                    predicate: Article.GetFilteredCategoryArticlesPredicate_2_0(
                        categoryId: request.CategoryId,
                        newsPortalIds: newsPortalIds,
                        searchTerm: request.TitleSearchQuery,
                        articleCreateDateTime: firstArticle?.CreateDateTime
                    ).Compile()
                )
                .FilterArticlesWithCoronaVirusContentForIosRelease(
                    deviceType: request.DeviceType,
                    targetedApiVersion: request.TargetedApiVersion
                )
                .Skip(request.Skip)
                .Take(request.Take);

            var articleDtos = filteredArticles.Select(GetCategoryArticlesArticle_2_0.GetProjection().Compile());

            var newsPortalDtos = GetNewNewsPortals(
                newsPortalIds: newsPortalIds,
                request: request
            );

            var random = new Random();

            var response = new GetCategoryArticlesQueryResponse_2_0
            {
                Articles = articleDtos,
                NewNewsPortals = newsPortalDtos.OrderBy(newsPortal => random.Next()),
                NewNewsPortalsPosition = request.NewNewsPortalsPosition
            };

            return Task.FromResult(result: response);
        }

        private IEnumerable<GetCategoryArticlesNewsPortal_2_0> GetNewNewsPortals(
            IEnumerable<int>? newsPortalIds,
            GetCategoryArticlesQuery_2_0 request
        )
        {
            if (request.Skip != 0)
            {
                return Array.Empty<GetCategoryArticlesNewsPortal_2_0>();
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
                .Select(selector: GetCategoryArticlesNewsPortal_2_0.GetProjection().Compile());

            return newsPortalDtos;
        }
        #endregion
    }
}

// GetCategoryArticlesQueryHandler_2_0.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Common.Extensions;
using Espresso.Domain.Entities;
using Espresso.Domain.Extensions;
using Espresso.Domain.Infrastructure;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_2_0
{
#pragma warning disable S101 // Types should be named in PascalCase
    public class GetCategoryArticlesQueryHandler_2_0 : IRequestHandler<GetCategoryArticlesQuery_2_0, GetCategoryArticlesQueryResponse_2_0>
#pragma warning restore S101 // Types should be named in PascalCase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ISettingProvider _settingProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCategoryArticlesQueryHandler_2_0"/> class.
        /// </summary>
        /// <param name="memoryCache"></param>
        /// <param name="settingProvider"></param>
        public GetCategoryArticlesQueryHandler_2_0(IMemoryCache memoryCache, ISettingProvider settingProvider)
        {
            _memoryCache = memoryCache;
            _settingProvider = settingProvider;
        }

        public Task<GetCategoryArticlesQueryResponse_2_0> Handle(
            GetCategoryArticlesQuery_2_0 request,
            CancellationToken cancellationToken)
        {
            var articles = _memoryCache.Get<IEnumerable<Article>>(
                key: MemoryCacheConstants.ArticleKey);

            var firstArticle = articles.FirstOrDefault(
                article => article.Id.Equals(request.FirstArticleId));

            var newsPortalIds = request.NewsPortalIds
                ?.Replace(" ", string.Empty)
                ?.Split(',')
                ?.Select(newsPortalIdString => int.TryParse(newsPortalIdString, out var newsPortalId) ? newsPortalId : default)
                ?.Where(newsPortalId => newsPortalId != default);

            var keyWordsToFilterOut = request.KeyWordsToFilterOut is null ?
                Enumerable.Empty<string>() :
                request.KeyWordsToFilterOut.Split(',', StringSplitOptions.RemoveEmptyEntries);

            var filteredArticles = articles
                .OrderArticlesByPublishDate()
                .FilterArticlesContainingKeyWords(keyWordsToFilterOut)
                .FilterArticles(
                    categoryId: request.CategoryId,
                    newsPortalIds: newsPortalIds,
                    titleSearchTerm: request.TitleSearchQuery,
                    articleCreateDateTime: firstArticle?.CreateDateTime)
                .Skip(request.Skip)
                .Take(request.Take);

            var articleDtos = filteredArticles.Select(GetCategoryArticlesArticle_2_0.GetProjection().Compile());

            var newsPortalDtos = GetNewNewsPortals(
                newsPortalIds: newsPortalIds,
                request: request);

            var random = new Random();

            var response = new GetCategoryArticlesQueryResponse_2_0
            {
                Articles = articleDtos,
                NewNewsPortals = newsPortalDtos.OrderBy(newsPortal => random.Next()),
                NewNewsPortalsPosition = _settingProvider.LatestSetting.NewsPortalSetting.NewNewsPortalsPosition,
            };

            return Task.FromResult(result: response);
        }

        private IEnumerable<GetCategoryArticlesNewsPortal_2_0> GetNewNewsPortals(
            IEnumerable<int>? newsPortalIds,
            GetCategoryArticlesQuery_2_0 request)
        {
            if (request.Skip != 0)
            {
                return Array.Empty<GetCategoryArticlesNewsPortal_2_0>();
            }

            var newsPortals = _memoryCache.Get<IEnumerable<NewsPortal>>(
                key: MemoryCacheConstants.NewsPortalKey);

            var newsPortalDtos = newsPortals
                .Where(
                    NewsPortal.GetCategorySugestedNewsPortalsPredicate(
                        newsPortalIds: newsPortalIds,
                        categoryId: request.CategoryId,
                        regionId: request.RegionId,
                        maxAgeOfNewNewsPortal: _settingProvider.LatestSetting.NewsPortalSetting.MaxAgeOfNewNewsPortal)
                    .Compile())
                .Select(selector: GetCategoryArticlesNewsPortal_2_0.GetProjection().Compile());

            return newsPortalDtos;
        }
    }
}

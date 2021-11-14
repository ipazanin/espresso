// GetFeaturedArticlesQueryHandler.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Common.Extensions;
using Espresso.Domain.Entities;
using Espresso.Domain.Extensions;
using Espresso.Domain.Infrastructure;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Espresso.WebApi.Application.Articles.Queries.GetFeaturedArticles
{
    public class GetFeaturedArticlesQueryHandler :
        IRequestHandler<GetFeaturedArticlesQuery, GetFeaturedArticlesQueryResponse>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ISettingProvider _settingProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFeaturedArticlesQueryHandler"/> class.
        /// </summary>
        /// <param name="memoryCache"></param>
        /// <param name="settingProvider"></param>
        public GetFeaturedArticlesQueryHandler(IMemoryCache memoryCache, ISettingProvider settingProvider)
        {
            _memoryCache = memoryCache;
            _settingProvider = settingProvider;
        }

        public Task<GetFeaturedArticlesQueryResponse> Handle(GetFeaturedArticlesQuery request, CancellationToken cancellationToken)
        {
            var articles = _memoryCache.Get<IEnumerable<Article>>(
                key: MemoryCacheConstants.ArticleKey);

            var categoryIds = request.CategoryIds
                ?.Replace(" ", string.Empty)
                ?.Split(',')
                ?.Select(categoryIdString => int.TryParse(categoryIdString, out var categoryId) ? categoryId : default)
                ?.Where(categoryId => categoryId != default);

            var keyWordsToFilterOut = request.KeyWordsToFilterOut is null ?
                Enumerable.Empty<string>() :
                request.KeyWordsToFilterOut.Split(',', StringSplitOptions.RemoveEmptyEntries);

            var featuredArticles = articles
                .FilterArticlesContainingKeyWords(keyWordsToFilterOut)
                .FilterFeaturedArticles(
                    categoryIds: null,
                    newsPortalIds: null,
                    maxAgeOfFeaturedArticle: _settingProvider.LatestSetting.ArticleSetting.MaxAgeOfFeaturedArticle,
                    articleCreateDateTime: null)
                .OrderFeaturedArticles(categoryIds);

            var trendingArticles = articles
                .FilterArticlesContainingKeyWords(keyWordsToFilterOut)
                .FilterTrendingArticles(
                    maxAgeOfTrendingArticle: _settingProvider.LatestSetting.ArticleSetting.MaxAgeOfTrendingArticle,
                    articleCreateDateTime: null)
                .OrderArticlesByTrendingScore();

            var articleDtos = featuredArticles
                .Union(trendingArticles)
                .Take(request.Take)
                .Select(GetFeaturedArticlesArticle.GetProjection().Compile());

            var response = new GetFeaturedArticlesQueryResponse
            {
                Articles = articleDtos,
            };

            return Task.FromResult(result: response);
        }
    }
}

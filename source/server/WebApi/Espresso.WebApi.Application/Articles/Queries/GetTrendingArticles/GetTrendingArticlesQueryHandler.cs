// GetTrendingArticlesQueryHandler.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Common.Extensions;
using Espresso.Domain.Entities;
using Espresso.Domain.Extensions;
using Espresso.Domain.Infrastructure;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles
{
    public class GetTrendingArticlesQueryHandler : IRequestHandler<GetTrendingArticlesQuery, GetTrendingArticlesQueryResponse>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ISettingProvider _settingProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTrendingArticlesQueryHandler"/> class.
        /// </summary>
        /// <param name="memoryCache"></param>
        /// <param name="settingProvider"></param>
        public GetTrendingArticlesQueryHandler(IMemoryCache memoryCache, ISettingProvider settingProvider)
        {
            _memoryCache = memoryCache;
            _settingProvider = settingProvider;
        }

        public Task<GetTrendingArticlesQueryResponse> Handle(GetTrendingArticlesQuery request, CancellationToken cancellationToken)
        {
            var articles = _memoryCache.Get<IEnumerable<Article>>(
                key: MemoryCacheConstants.ArticleKey);

            var firstArticle = articles.FirstOrDefault(
                article => article.Id.Equals(request.FirstArticleId));

            var articleDtos = articles
                .FilterTrendingArticles(
                    maxAgeOfTrendingArticle: _settingProvider.LatestSetting.ArticleSetting.MaxAgeOfTrendingArticle,
                    articleCreateDateTime: firstArticle?.CreateDateTime)
                .OrderArticlesByTrendingScore()
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(GetTrendingArticlesArticle.GetProjection().Compile());

            var response = new GetTrendingArticlesQueryResponse
            {
                Articles = articleDtos,
            };

            return Task.FromResult(result: response);
        }
    }
}

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

namespace Espresso.WebApi.Application.Articles.Queries.GetFeaturedArticles
{
    public class GetFeaturedArticlesQueryHandler :
        IRequestHandler<GetFeaturedArticlesQuery, GetFeaturedArticlesQueryResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Constructors
        public GetFeaturedArticlesQueryHandler(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<GetFeaturedArticlesQueryResponse> Handle(GetFeaturedArticlesQuery request, CancellationToken cancellationToken)
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

            var categoryIds = request.CategoryIds
                ?.Replace(" ", "")
                ?.Split(',')
                ?.Select(categoryIdString => int.TryParse(categoryIdString, out var categoryId) ? categoryId : default)
                ?.Where(categoryId => categoryId != default);

            var featuredArticles = articles
                .FilterFeaturedArticles(
                    categoryIds: null,
                    newsPortalIds: null,
                    maxAgeOfFeaturedArticle: request.MaxAgeOfFeaturedArticle,
                    articleCreateDateTime: null
                )
                // .FilterArticlesWithCoronaVirusContentForIosRelease(request.DeviceType, request.TargetedApiVersion)
                .OrderFeaturedArticles(categoryIds);

            var trendingArticles = articles
                .FilterTrendingArticles(
                    maxAgeOfTrendingArticle: request.MaxAgeOfTrendingArticle,
                    articleCreateDateTime: null
                )
                // .FilterArticlesWithCoronaVirusContentForIosRelease(request.DeviceType, request.TargetedApiVersion)
                .OrderArticlesByTrendingScore();

            var articleDtos = featuredArticles
                .Union(trendingArticles)
                .Take(request.Take)
                .Select(GetFeaturedArticlesArticle.GetProjection().Compile());

            var response = new GetFeaturedArticlesQueryResponse
            {
                Articles = articleDtos
            };

            return Task.FromResult(result: response);
        }
        #endregion
    }
}

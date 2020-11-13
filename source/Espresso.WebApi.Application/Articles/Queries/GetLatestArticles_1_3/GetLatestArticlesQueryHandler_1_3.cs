using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Common.Extensions;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;
using Espresso.Domain.Extensions;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_3
{
    public class GetLatestArticlesQueryHandler_1_3 : IRequestHandler<GetLatestArticlesQuery_1_3, GetLatestArticlesQueryResponse_1_3>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Constructors
        public GetLatestArticlesQueryHandler_1_3(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<GetLatestArticlesQueryResponse_1_3> Handle(
            GetLatestArticlesQuery_1_3 request,
            CancellationToken cancellationToken
        )
        {
            var articles = _memoryCache.Get<IEnumerable<Article>>(
                key: MemoryCacheConstants.ArticleKey
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

            var articleDtos = articles
                .OrderArticlesByPublishDate()
                .FilterArticles_2_0(
                    categoryIds: categoryIds,
                    newsPortalIds: newsPortalIds,
                    titleSearchTerm: request.TitleSearchQuery,
                    articleCreateDateTime: null
                )
                .Where(
                    article => !article.ArticleCategories
                        .Any(articleCategory => articleCategory.CategoryId.Equals((int)CategoryId.Local))
                )
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(GetLatestArticlesArticle_1_3.GetProjection().Compile());

            var response = new GetLatestArticlesQueryResponse_1_3
            {
                Articles = articleDtos
            };

            return Task.FromResult(result: response);
        }
        #endregion
    }
}

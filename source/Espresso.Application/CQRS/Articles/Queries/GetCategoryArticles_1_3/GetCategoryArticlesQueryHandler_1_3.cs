﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles_1_3
{
    public class GetCategoryArticlesQueryHandler_1_3 : IRequestHandler<GetCategoryArticlesQuery_1_3, GetCategoryArticlesQueryResponse_1_3>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Contructors
        public GetCategoryArticlesQueryHandler_1_3(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<GetCategoryArticlesQueryResponse_1_3> Handle(GetCategoryArticlesQuery_1_3 request, CancellationToken cancellationToken)
        {
            var articles = _memoryCache.Get<IEnumerable<Article>>(
                key: MemoryCacheConstants.ArticleKey
            );

            var articleDtos = articles
                .OrderByDescending(keySelector: Article.GetArticleOrderByDescendingExpression().Compile())
                .Where(
                    predicate: Article.GetCategoryArticlePredicate(
                        categoryId: request.CategoryId,
                        newsPortalIds: request.NewsPortalIds
                    ).Compile()
                )
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(GetCategoryArticlesArticle_1_3.GetProjection().Compile());

            var response = new GetCategoryArticlesQueryResponse_1_3(
                articles: articleDtos
            );

            return Task.FromResult(result: response);
        }
        #endregion
    }
}
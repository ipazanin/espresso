using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Articles.AutoCompleteArticle
{
    public class AutoCompleteArticleQueryHandler : IRequestHandler<AutoCompleteArticleQuery, AutoCompleteArticleQueryResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Constructors
        public AutoCompleteArticleQueryHandler(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<AutoCompleteArticleQueryResponse> Handle(AutoCompleteArticleQuery request, CancellationToken cancellationToken)
        {
            var articles = _memoryCache.Get<IEnumerable<Article>>(MemoryCacheConstants.ArticleKey);

            var candidateArticles = articles
                .Where(Article.GetAutocompleteArticleTitleExpression(request.TitleSearchQuery).Compile());

            var articleTitles = candidateArticles
                .Select(article => article.Title)
                .Skip(request.Skip)
                .Take(request.Take);

            var result = new AutoCompleteArticleQueryResponse
            {
                ArticleTitles = articleTitles
            };

            return Task.FromResult(result);
        }
        #endregion
    }
}
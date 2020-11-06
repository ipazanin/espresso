using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application
{
    public class UpdateInMemorySimilarArticlesCommandHandler : IRequestHandler<UpdateInMemorySimilarArticlesCommand, UpdateInMemorySimilarArticlesCommandResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Constructors
        public UpdateInMemorySimilarArticlesCommandHandler(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<UpdateInMemorySimilarArticlesCommandResponse> Handle(UpdateInMemorySimilarArticlesCommand request, CancellationToken cancellationToken)
        {
            var articlesDictionary = _memoryCache
                .Get<IEnumerable<Article>>(key: MemoryCacheConstants.ArticleKey)
                .ToDictionary(article => article.Id);

            foreach (var similarArticleDto in request.SimilarArticles)
            {
                if (
                    articlesDictionary.TryGetValue(similarArticleDto.MainArticleId, out var mainArticle) &&
                    articlesDictionary.TryGetValue(similarArticleDto.SubordinateArticleId, out var subordinateArticle)
                )
                {
                    var similarArticle = similarArticleDto.CreateSimilarArticle(
                        mainArticle: mainArticle,
                        subordinateArticle: subordinateArticle
                    );

                    mainArticle.SubordinateArticles.Add(similarArticle);
                    subordinateArticle.SetMainArticle(similarArticle);
                }
            }

            _memoryCache.Set(key: MemoryCacheConstants.ArticleKey, value: articlesDictionary.Values.ToList());

            var response = new UpdateInMemorySimilarArticlesCommandResponse
            {
                SimilarArticlesCount = request.SimilarArticles.Count()
            };

            return Task.FromResult(response);
        }
        #endregion
    }
}
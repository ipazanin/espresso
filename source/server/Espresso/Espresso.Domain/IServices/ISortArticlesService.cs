// ISortArticlesService.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Espresso.Domain.Entities;

namespace Espresso.Domain.IServices
{
    public interface ISortArticlesService
    {
        public (
            IEnumerable<Article> createdArticles,
            IEnumerable<(Article article, IEnumerable<string> modifiedProperties)> updatedArticlesWithModifiedProperties,
            IEnumerable<ArticleCategory> createArticleCategories,
            IEnumerable<ArticleCategory> deleteArticleCategories
        ) SortArticles(
            IEnumerable<Article> articles,
            IDictionary<Guid, Article> savedArticles
        );

        /// <summary>
        ///
        /// </summary>
        /// <param name="articlesChannel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<IEnumerable<Article>> RemoveDuplicateArticles(Channel<Article> articlesChannel, CancellationToken cancellationToken);
    }
}

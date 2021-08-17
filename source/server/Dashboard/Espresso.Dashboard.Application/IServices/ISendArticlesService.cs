// ISendArticlesService.cs
//
// � 2021 Espresso News. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Domain.Entities;

namespace Espresso.Dashboard.Application.IServices
{
    public interface ISendArticlesService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="createArticles"></param>
        /// <param name="updateArticles"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public Task SendArticlesMessage(
            IEnumerable<Article> createArticles,
            IEnumerable<Article> updateArticles,
            CancellationToken cancellationToken
        );
    }
}
// ICreateArticleService.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Espresso.Domain.Entities;
using Espresso.Domain.Records;

namespace Espresso.Domain.IServices
{
    public interface ICreateArticleService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="rssFeedItems"></param>
        /// <param name="categories"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<Channel<Article>> CreateArticlesFromRssFeedItems(
            Channel<RssFeedItem> rssFeedItems,
            IEnumerable<Category> categories,
            CancellationToken cancellationToken
        );
    }
}

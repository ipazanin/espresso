// ICreateArticleService.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.Records;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Espresso.Domain.IServices
{
    public interface ICreateArticleService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="rssFeedItemChannel"></param>
        /// <param name="categories"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<Channel<Article>> CreateArticlesFromRssFeedItems(
            Channel<RssFeedItem> rssFeedItemChannel,
            IEnumerable<Category> categories,
            CancellationToken cancellationToken);
    }
}

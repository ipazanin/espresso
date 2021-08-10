// ILoadRssFeedsService.cs
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
    public interface ILoadRssFeedsService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="rssFeeds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<Channel<RssFeedItem>> ParseRssFeeds(
            IEnumerable<RssFeed> rssFeeds,
            CancellationToken cancellationToken
        );
    }
}

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
        public Task<Channel<RssFeedItem>> ParseRssFeeds(
            IEnumerable<RssFeed> rssFeeds,
            CancellationToken cancellationToken
        );
    }
}

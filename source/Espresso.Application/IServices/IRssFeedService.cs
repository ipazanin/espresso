using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Enums;
using Espresso.Domain.Entities;

namespace Espresso.Application.IService
{
    public interface IRssFeedService
    {
        public Task<IEnumerable<(SyndicationFeed SyndicationFeed, RssFeed rssFeed)>> ParseRssFeeds(
            IEnumerable<RssFeed> rssFeeds,
            AppEnvironment appEnvironment,
            string currentApiVersion,
            CancellationToken cancellationToken
        );
    }
}

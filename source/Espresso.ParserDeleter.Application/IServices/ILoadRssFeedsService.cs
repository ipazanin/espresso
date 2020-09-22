using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Enums;
using Espresso.Domain.Entities;
using Espresso.Domain.Records;

namespace Espresso.ParserDeleter.Application.IServices
{
    public interface ILoadRssFeedsService
    {
        public Task<IEnumerable<RssFeedItem>> ParseRssFeeds(
            IEnumerable<RssFeed> rssFeeds,
            AppEnvironment appEnvironment,
            string currentApiVersion,
            CancellationToken cancellationToken
        );
    }
}

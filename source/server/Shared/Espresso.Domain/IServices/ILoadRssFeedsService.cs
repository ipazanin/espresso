// ILoadRssFeedsService.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.Records;

namespace Espresso.Domain.IServices;

public interface ILoadRssFeedsService
{
    public ChannelReader<RssFeedItem> RssFeedItemsChannelReader { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="rssFeeds"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public Task ParseRssFeeds(
        IEnumerable<RssFeed> rssFeeds,
        CancellationToken cancellationToken);
}

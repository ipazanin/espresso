// ICreateArticlesService.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.Records;

namespace Espresso.Domain.IServices;

public interface ICreateArticlesService
{
    public ChannelReader<Article> ArticlesChannelReader { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="rssFeedItemChannelReader"></param>
    /// <param name="categories"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public Task CreateArticlesFromRssFeedItems(
        ChannelReader<RssFeedItem> rssFeedItemChannelReader,
        IEnumerable<Category> categories,
        CancellationToken cancellationToken);
}

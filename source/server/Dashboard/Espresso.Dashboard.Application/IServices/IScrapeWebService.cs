// IScrapeWebService.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;

namespace Espresso.Dashboard.Application.IServices;

public interface IScrapeWebService
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="articleUrl"></param>
    /// <param name="rssFeed"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public Task<string?> GetSrcAttributeFromElementDefinedByXPath(
        string? articleUrl,
        RssFeed rssFeed,
        CancellationToken cancellationToken);
}

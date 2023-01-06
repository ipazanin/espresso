// ISendInformationToApiService.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.Dashboard.Application.IServices;

public interface ISendInformationToApiService
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="createArticleIds"></param>
    /// <param name="updateArticleIds"></param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public Task SendArticlesMessage(
        IEnumerable<Guid> createArticleIds,
        IEnumerable<Guid> updateArticleIds);

    /// <summary>
    /// Notifies application API that settings has changed.
    /// </summary>
    public Task SendSettingUpdatedNotification();

    /// <summary>
    /// Notifies application API that cache (RssFeed, Categories, Regions...) has changed.
    /// </summary>
    public Task SendCacheUpdatedNotification();
}

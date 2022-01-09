// ISendArticlesService.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Dashboard.Application.IServices;

public interface ISendArticlesService
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="createArticleIds"></param>
    /// <param name="updateArticleIds"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public Task SendArticlesMessage(
        IEnumerable<Guid> createArticleIds,
        IEnumerable<Guid> updateArticleIds,
        CancellationToken cancellationToken);
}

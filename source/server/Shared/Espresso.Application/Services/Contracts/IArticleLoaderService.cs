// IArticleLoaderService.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;

namespace Espresso.Application.Services.Contracts;

public interface IArticleLoaderService
{
    public Task<IReadOnlyList<Article>> LoadArticlesForWebApi(
        IReadOnlyList<NewsPortal> newsPortals,
        IReadOnlyList<Category> categories,
        CancellationToken cancellationToken);

    public Task<IReadOnlyList<Article>> LoadArticlesForWebApi(
        ISet<Guid> articleIds,
        IReadOnlyList<NewsPortal> newsPortals,
        IReadOnlyList<Category> categories,
        CancellationToken cancellationToken);
}

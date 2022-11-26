// IArticleLoaderService.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;

namespace Espresso.Application.Services.Contracts;

public interface IArticleLoaderService
{
    public Task<IEnumerable<Article>> LoadArticlesForWebApi(
        IEnumerable<NewsPortal> newsPortals,
        IEnumerable<Category> categories,
        CancellationToken cancellationToken);

    public Task<IEnumerable<Article>> LoadArticlesForWebApi(
        ISet<Guid> articleIds,
        IEnumerable<NewsPortal> newsPortals,
        IEnumerable<Category> categories,
        CancellationToken cancellationToken);
}

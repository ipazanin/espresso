// IRemoveOldArticlesService.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;

namespace Espresso.Domain.IServices;

public interface IRemoveOldArticlesService
{
    public IReadOnlyList<Article> RemoveOldArticlesFromCollection(IDictionary<Guid, Article> articles);

    public IReadOnlyList<Article> RemoveOldArticles(IReadOnlyList<Article> articles);
}

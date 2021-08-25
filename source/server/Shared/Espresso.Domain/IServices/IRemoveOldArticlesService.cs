// IRemoveOldArticlesService.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Espresso.Domain.IServices
{
    public interface IRemoveOldArticlesService
    {
        public IEnumerable<Article> RemoveOldArticlesFromCollection(IDictionary<Guid, Article> articles);

        public IEnumerable<Article> RemoveOldArticles(IEnumerable<Article> articles);
    }
}

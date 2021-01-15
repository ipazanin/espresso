using System;
using System.Collections.Generic;
using Espresso.Domain.Entities;

namespace Espresso.Domain.IServices
{
    public interface IRemoveOldArticlesService
    {
        public int RemoveOldArticlesFromCollection(IDictionary<Guid, Article> articles);

        public IEnumerable<Article> RemoveOldArticles(IEnumerable<Article> articles);
    }
}

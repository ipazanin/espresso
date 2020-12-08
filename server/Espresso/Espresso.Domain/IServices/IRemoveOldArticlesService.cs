using System.Collections.Generic;
using Espresso.Domain.Entities;

namespace Espresso.Domain.IServices
{
    public interface IRemoveOldArticlesService
    {
        public IEnumerable<Article> RemoveOldArticles(IEnumerable<Article> articles);
    }
}
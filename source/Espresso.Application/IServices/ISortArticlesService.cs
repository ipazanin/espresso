using System.Collections.Generic;
using Espresso.Domain.Entities;

namespace Espresso.Application.IService
{
    public interface ISortArticlesService
    {
        public (IEnumerable<Article> createdArticles, IEnumerable<Article> updatedArticles) SortArticles(
            IEnumerable<Article> articles,
            IEnumerable<Article> savedArticles
        );

        public IEnumerable<Article> RemoveDuplicateArticles(IEnumerable<Article> articles);
    }
}
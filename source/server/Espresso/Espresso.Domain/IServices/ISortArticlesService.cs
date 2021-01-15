using System;
using System.Collections.Generic;
using Espresso.Domain.Entities;

namespace Espresso.Domain.IServices
{
    public interface ISortArticlesService
    {
        public (
            IEnumerable<Article> createdArticles,
            IEnumerable<Article> updatedArticles,
            IEnumerable<ArticleCategory> articleCategoriesToCreate,
            IEnumerable<ArticleCategory> articleCategoriesToDelete
        ) SortArticles(
            IEnumerable<Article> articles,
            IDictionary<Guid, Article> savedArticles
        );

        public IEnumerable<Article> RemoveDuplicateArticles(IEnumerable<Article> articles);
    }
}

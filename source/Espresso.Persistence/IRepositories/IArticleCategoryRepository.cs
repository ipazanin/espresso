using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Espresso.Domain.Entities;

namespace Espresso.Persistence.IRepositories
{
    public interface IArticleCategoryRepository
    {
        public Task<IEnumerable<ArticleCategory>> GetArticleCategories();

        public void InsertArticleCategories(IEnumerable<ArticleCategory> articleCategories);

        public void DeleteArticleCategories(IEnumerable<Guid> articleCategoryIds);
    }
}

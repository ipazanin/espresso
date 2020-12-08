using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Espresso.Domain.Entities;

namespace Espresso.Persistence.IRepositories
{
    public interface IArticleRepository
    {
        public void InsertArticles(IEnumerable<Article> articles);

        public void UpdateArticles(IEnumerable<Article> articles);

        public int DeleteArticlesAndSimilarArticles(DateTime maxArticleAge);
    }
}

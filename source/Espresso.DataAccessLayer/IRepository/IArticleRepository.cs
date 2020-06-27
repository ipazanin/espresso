using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Espresso.Domain.Entities;

namespace Espresso.DataAccessLayer.IRepository
{
    public interface IArticleRepository
    {
        public Task<IEnumerable<Article>> GetArticles();

        public void InsertArticles(IEnumerable<Article> articles);

        public void UpdateArticles(IEnumerable<Article> articles);

        public int DeleteArticles(DateTime maxArticleAge);
    }
}

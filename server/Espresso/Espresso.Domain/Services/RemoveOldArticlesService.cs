using System;
using System.Collections.Generic;
using System.Linq;
using Espresso.Domain.Entities;
using Espresso.Domain.IServices;

namespace Espresso.Domain.Services
{
    public class RemoveOldArticlesService : IRemoveOldArticlesService
    {
        #region Fields
        private readonly TimeSpan _maxAgeOfArticle;
        #endregion

        #region Constructors
        public RemoveOldArticlesService(
            TimeSpan maxAgeOfArticle
        )
        {
            _maxAgeOfArticle = maxAgeOfArticle;
        }
        #endregion

        #region Methods
        public IEnumerable<Article> RemoveOldArticles(IEnumerable<Article> articles)
        {
            var maxAgeDate = DateTime.UtcNow - _maxAgeOfArticle;
            var oldArticles = articles.Where(article => article.PublishDateTime < maxAgeDate);

            var articlesDictionary = articles.ToDictionary(article => article.Id);

            foreach (var oldArticle in oldArticles)
            {
                articlesDictionary.Remove(oldArticle.Id);
                oldArticle.RemoveSimilarArticles();
            }

            return articlesDictionary.Values;
        }
        #endregion
    }
}
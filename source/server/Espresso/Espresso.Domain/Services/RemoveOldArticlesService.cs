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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="articles"></param>
        /// <returns>Removed articles</returns>
        public IEnumerable<Article> RemoveOldArticlesFromCollection(IDictionary<Guid, Article> articles)
        {
            var maxAgeDate = DateTime.UtcNow - _maxAgeOfArticle;
            var articlesToRemove = new List<Article>();

            foreach (var (_, article) in articles)
            {
                if (article.PublishDateTime < maxAgeDate)
                {
                    articlesToRemove.Add(article);
                }
            }
            foreach (var article in articlesToRemove)
            {
                articles.Remove(article.Id);
            }

            return articlesToRemove;
        }

        public IEnumerable<Article> RemoveOldArticles(IEnumerable<Article> articles)
        {
            var maxAgeDate = DateTime.UtcNow - _maxAgeOfArticle;

            var notOldArticles = articles.Where(article => article.PublishDateTime > maxAgeDate);

            return notOldArticles;
        }
        #endregion
    }
}

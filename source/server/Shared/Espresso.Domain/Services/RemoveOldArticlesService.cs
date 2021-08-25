// RemoveOldArticlesService.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Espresso.Domain.Services
{
    public class RemoveOldArticlesService : IRemoveOldArticlesService
    {
        private readonly TimeSpan _maxAgeOfArticle;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveOldArticlesService"/> class.
        /// </summary>
        /// <param name="maxAgeOfArticle"></param>
        public RemoveOldArticlesService(
            TimeSpan maxAgeOfArticle
        )
        {
            _maxAgeOfArticle = maxAgeOfArticle;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articles"></param>
        /// <returns>Removed articles.</returns>
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
    }
}

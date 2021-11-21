// RemoveOldArticlesService.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.Infrastructure;
using Espresso.Domain.IServices;

namespace Espresso.Domain.Services
{
    public class RemoveOldArticlesService : IRemoveOldArticlesService
    {
        private readonly ISettingProvider _settingProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveOldArticlesService"/> class.
        /// </summary>
        /// <param name="settingProvider"></param>
        public RemoveOldArticlesService(ISettingProvider settingProvider)
        {
            _settingProvider = settingProvider;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="articles"></param>
        /// <returns>Removed articles.</returns>
        public IEnumerable<Article> RemoveOldArticlesFromCollection(IDictionary<Guid, Article> articles)
        {
            var maxAgeDate = DateTimeOffset.UtcNow - _settingProvider.LatestSetting.ArticleSetting.MaxAgeOfArticle;
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
                _ = articles.Remove(article.Id);
            }

            return articlesToRemove;
        }

        public IEnumerable<Article> RemoveOldArticles(IEnumerable<Article> articles)
        {
            var maxAgeDate = DateTimeOffset.UtcNow - _settingProvider.LatestSetting.ArticleSetting.MaxAgeOfArticle;

            var notOldArticles = articles.Where(article => article.PublishDateTime > maxAgeDate);

            return notOldArticles;
        }
    }
}

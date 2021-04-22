using System;
using System.Collections.Generic;
using Espresso.Domain.Entities;
using Espresso.Domain.ValueObjects.ArticleValueObjects;

namespace Espresso.Domain.Tests.TestUtilities
{
    /// <summary>
    /// ArticleUtility
    /// </summary>
    public static class ArticleUtility
    {
        #region Methods
        public static Article CreateDefaultArticleWith(
            int newsPortalId = default,
            string url = "",
            string title = "",
            string summary = "",
            DateTime publishDateTime = default,
            DateTime createDateTime = default,
            int categoryId = default,
            Guid id = default
        )
        {
            var category = new Category(
                id: categoryId,
                name: "",
                color: "",
                keyWordsRegexPattern: default,
                sortIndex: default,
                position: default,
                categoryType: default,
                categoryUrl: ""
            );

            var articleCategories = new List<ArticleCategory>()
            {
                new ArticleCategory(
                    id: default,
                    article: null,
                    articleId: default,
                    category: category,
                    categoryId: default
                )
            };

            var newsPortal = new NewsPortal(
                id: newsPortalId,
                name: "",
                baseUrl: "",
                iconUrl: "",
                isNewOverride: null,
                createdAt: default,
                categoryId: default,
                regionId: default,
                isEnabled: true
            );

            var article = new Article(
                id: id,
                url: url,
                webUrl: "",
                summary: summary,
                title: title,
                imageUrl: default,
                createDateTime: createDateTime,
                updateDateTime: default,
                publishDateTime: publishDateTime,
                numberOfClicks: default,
                trendingScore: default,
                editorConfiguration: new EditorConfiguration(),
                newsPortalId: newsPortal.Id,
                rssFeedId: default,
                articleCategories: articleCategories,
                newsPortal: newsPortal,
                rssFeed: default,
                subordinateArticles: default,
                mainArticle: default
            );

            return article;
        }
        #endregion Methods
    }
}

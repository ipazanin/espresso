using System;
using System.Collections.Generic;
using System.Linq;
using Espresso.Domain.Entities;
using Espresso.Domain.Services;
using Espresso.Domain.Tests.TestUtilities;
using Xunit;

namespace Espresso.Domain.Tests.Services
{
    /// <summary>
    /// RemoveOldArticlesService
    /// </summary>
    public class RemoveOldArticlesServiceTest
    {
        #region RemoveOldArticles
        [Fact]
        public void RemoveOldArticles_ReturnsUnchangedNumberOfArticles_WhenNoArticlesAreOlderThanProvidedThreshold()
        {
            #region Arrange
            var articles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    publishDateTime: DateTime.UtcNow
                ),
            };
            var expectedArticlesCount = articles.Count;

            var sortArticlesService = new RemoveOldArticlesService(
                maxAgeOfArticle: TimeSpan.FromHours(1)
            );
            #endregion Arrange

            #region Act
            var actualArticles = sortArticlesService.RemoveOldArticles(articles: articles);
            #endregion Act

            #region Assert
            var actualArticlesCount = actualArticles.Count();
            Assert.Equal(
                expected: expectedArticlesCount,
                actual: actualArticlesCount
            );
            #endregion Assert
        }

        [Fact]
        public void RemoveOldArticles_ReturnsOneLessArticle_WhenOneArticleIsOlderThanProvidedPublishDateTimeThreshold()
        {
            #region Arrange
            var articles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    publishDateTime: DateTime.UtcNow.AddHours(-2)
                ),
            };
            var expectedArticlesCount = articles.Count - 1;

            var sortArticlesService = new RemoveOldArticlesService(
                maxAgeOfArticle: TimeSpan.FromHours(1)
            );
            #endregion Arrange

            #region Act
            var actualArticles = sortArticlesService.RemoveOldArticles(articles: articles);
            #endregion Act

            #region Assert
            var actualArticlesCount = actualArticles.Count();
            Assert.Equal(
                expected: expectedArticlesCount,
                actual: actualArticlesCount
            );
            #endregion Assert
        }
        #endregion RemoveOldArticles
    }
}
// RemoveOldArticlesServiceTest.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.Services;
using Espresso.Domain.Tests.TestUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Espresso.Domain.Tests.Services
{
    /// <summary>
    /// RemoveOldArticlesService.
    /// </summary>
    public class RemoveOldArticlesServiceTest
    {
        [Fact]
        public void RemoveOldArticles_ReturnsUnchangedNumberOfArticles_WhenNoArticlesAreOlderThanProvidedThreshold()
        {
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

            var actualArticles = sortArticlesService.RemoveOldArticles(articles: articles);

            var actualArticlesCount = actualArticles.Count();
            Assert.Equal(
                expected: expectedArticlesCount,
                actual: actualArticlesCount
            );
        }

        [Fact]
        public void RemoveOldArticles_ReturnsOneLessArticle_WhenOneArticleIsOlderThanProvidedPublishDateTimeThreshold()
        {
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

            var actualArticles = sortArticlesService.RemoveOldArticles(articles: articles);

            var actualArticlesCount = actualArticles.Count();
            Assert.Equal(
                expected: expectedArticlesCount,
                actual: actualArticlesCount
            );
        }
    }
}

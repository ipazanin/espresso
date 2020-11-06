using System;
using System.Collections.Generic;
using System.Linq;
using Espresso.Domain.Entities;
using Espresso.Domain.IServices;
using Espresso.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Espresso.Domain.Tests.Services
{
    public class GroupSimilarArticlesServiceTest
    {

        #region GroupSimilarArticles
        [Fact]
        public void GroupSimilarArticles_GroupsTwoSimilarArticlesWithSameTitle()
        {
            #region Arrange
            var loggerServiceMock = new Mock<ILoggerService<GroupSimilarArticlesService>>(MockBehavior.Strict);

            loggerServiceMock.Setup(loggerService => loggerService.Log(
                It.IsAny<string>(),
                It.IsAny<LogLevel>(),
                It.IsAny<IEnumerable<(string argumentName, object argumentValue)>>()
            ));

            var expectedSimilarArticlesCount = 1;

            var articles = new List<Article>
            {
                new Article(
                    id: Guid.NewGuid(),
                    url: default!,
                    webUrl: default!,
                    summary: default!,
                    title: "Very Cool Title",
                    imageUrl: default,
                    createDateTime: DateTime.UtcNow,
                    updateDateTime: default,
                    publishDateTime: DateTime.UtcNow,
                    numberOfClicks: default,
                    trendingScore: default,
                    editorConfiguration: default!,
                    newsPortalId: 1,
                    rssFeedId: default,
                    articleCategories: new List<ArticleCategory>(){
                        new ArticleCategory(
                            id: default,
                            article: null,
                            articleId: default,
                            category: new Category(
                                id: default,
                                name: "Category1",
                                color: default!,
                                keyWordsRegexPattern: default,
                                sortIndex: default,
                                position: default,
                                categoryType: default,
                                categoryUrl: default!
                            ),
                            categoryId: default
                        )
                    },
                    newsPortal: new NewsPortal(
                        id: 2,
                        name: "Name2",
                        baseUrl: default!,
                        iconUrl: default!,
                        isNewOverride: null,
                        createdAt: default,
                        categoryId: default,
                        regionId: default
                    ),
                    rssFeed: default,
                    subordinateArticles: default,
                    mainArticle: default
                ),
                new Article(
                    id: Guid.NewGuid(),
                    url: default!,
                    webUrl: default!,
                    summary: default!,
                    title: "Very Cool Title",
                    imageUrl: default,
                    createDateTime: DateTime.UtcNow,
                    updateDateTime: default,
                    publishDateTime: DateTime.UtcNow,
                    numberOfClicks: default,
                    trendingScore: default,
                    editorConfiguration: default!,
                    newsPortalId: 2,
                    rssFeedId: default,
                    articleCategories: new List<ArticleCategory>(){
                        new ArticleCategory(
                            id: default,
                            article: null,
                            articleId: default,
                            category: new Category(
                                id: default,
                                name: "Category1",
                                color: default!,
                                keyWordsRegexPattern: default,
                                sortIndex: default,
                                position: default,
                                categoryType: default,
                                categoryUrl: default!
                            ),
                            categoryId: default
                        )
                    },
                    newsPortal: new NewsPortal(
                        id: 1,
                        name: "Name",
                        baseUrl: default!,
                        iconUrl: default!,
                        isNewOverride: null,
                        createdAt: default,
                        categoryId: default,
                        regionId: default
                    ),
                    rssFeed: default,
                    subordinateArticles: default,
                    mainArticle: default
                ),
            };
            var service = new GroupSimilarArticlesService(
                similarityScoreThreshold: 0.5,
                articlePublishDateTimeDiferenceThreshold: TimeSpan.FromHours(16),
                loggerService: loggerServiceMock.Object,
                maxAgeOfSimilarArticleChecking: TimeSpan.FromHours(32),
                groupSimilarArticlesBatchSize: 100
            );
            #endregion

            #region Act
            var similarArticles = service.GroupSimilarArticles(articles, new DateTime());
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedSimilarArticlesCount,
                actual: similarArticles.Count()
            );
            #endregion
        }
        #endregion
    }
}
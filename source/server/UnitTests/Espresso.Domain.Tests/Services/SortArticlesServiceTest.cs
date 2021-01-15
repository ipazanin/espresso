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
    /// SortArticlesServiceTest
    /// </summary>
    public class SortArticlesServiceTest
    {
        #region SortArticles
        [Fact]
        public void SortArticles_ReturnsEmptyCreateArticles_WhenArticlesCollectionIsEmpty()
        {
            #region Arrange
            var articles = new List<Article>();
            var savedArticles = new List<Article>()
                .ToDictionary(article => article.Id);

            var expectedCreatedArticles = new List<Article>();

            var sortArticlesService = new SortArticlesService();
            #endregion Arrange

            #region Act
            var (actualCreatedArticles, _, _, _) = sortArticlesService.SortArticles(
                articles: articles,
                savedArticles: savedArticles
            );
            #endregion Act

            #region Assert
            Assert.Equal(
                expected: expectedCreatedArticles,
                actual: actualCreatedArticles
            );
            #endregion Assert
        }

        [Fact]
        public void SortArticles_ReturnsEmptyUpdateArticles_WhenArticlesCollectionIsEmpty()
        {
            #region Arrange
            var articles = new List<Article>();
            var savedArticles = new List<Article>()
                .ToDictionary(article => article.Id);

            var expectedUpdateArticles = new List<Article>();

            var sortArticlesService = new SortArticlesService();
            #endregion Arrange

            #region Act
            var (_, actualUpdateArticles, _, _) = sortArticlesService.SortArticles(
                articles: articles,
                savedArticles: savedArticles
            );
            #endregion Act

            #region Assert
            Assert.Equal(
                expected: expectedUpdateArticles,
                actual: actualUpdateArticles
            );
            #endregion Assert
        }

        [Fact]
        public void SortArticles_ReturnsEmptyCreateArticleCategories_WhenArticlesCollectionIsEmpty()
        {
            #region Arrange
            var articles = new List<Article>();
            var savedArticles = new List<Article>()
                .ToDictionary(article => article.Id);

            var expectedCreatedArticleCategories = new List<ArticleCategory>();

            var sortArticlesService = new SortArticlesService();
            #endregion Arrange

            #region Act
            var (_, _, actualCreatedArticleCategories, _) = sortArticlesService.SortArticles(
                articles: articles,
                savedArticles: savedArticles
            );
            #endregion Act

            #region Assert
            Assert.Equal(
                expected: expectedCreatedArticleCategories,
                actual: actualCreatedArticleCategories
            );
            #endregion Assert
        }

        [Fact]
        public void SortArticles_ReturnsEmptyUpdateArticleCategories_WhenArticlesCollectionIsEmpty()
        {
            #region Arrange
            var articles = new List<Article>();
            var savedArticles = new List<Article>()
                .ToDictionary(article => article.Id);

            var expectedUpdateArticleCategories = new List<ArticleCategory>();

            var sortArticlesService = new SortArticlesService();
            #endregion Arrange

            #region Act
            var (_, _, _, actualUpdateArticleCategories) = sortArticlesService.SortArticles(
                articles: articles,
                savedArticles: savedArticles
            );
            #endregion Act

            #region Assert
            Assert.Equal(
                expected: expectedUpdateArticleCategories,
                actual: actualUpdateArticleCategories
            );
            #endregion Assert
        }

        [Fact]
        public void SortArticles_ReturnsCreateArticlesWithSingleArticle_WhenArticlesCollectionHasSingleArticleAndSavedArticlesCollectionIsEmpty()
        {
            #region Arrange
            var articles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(default, default!, default!, default!)
            };
            var savedArticles = new List<Article>()
                .ToDictionary(article => article.Id);

            var expectedCreatedArticlesCount = 1;

            var sortArticlesService = new SortArticlesService();
            #endregion Arrange

            #region Act
            var (actualCreatedArticles, _, _, _) = sortArticlesService.SortArticles(
                articles: articles,
                savedArticles: savedArticles
            );
            #endregion Act

            #region Assert
            var actualCreatedArticlesCount = actualCreatedArticles.Count();
            Assert.Equal(
                expected: expectedCreatedArticlesCount,
                actual: actualCreatedArticlesCount
            );
            #endregion Assert
        }

        [Fact]
        public void SortArticles_ReturnsCreateArticleCategoriesWithSingleArticleCategory_WhenArticlesCollectionHasSingleArticleWithSingleArticleCategoryAndSavedArticlesCollectionIsEmpty()
        {
            #region Arrange
            var articles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(default, default!, default!, default!)
            };
            var savedArticles = new List<Article>()
                .ToDictionary(article => article.Id);

            var expectedCreatedArticleCategoriesCount = 1;

            var sortArticlesService = new SortArticlesService();
            #endregion Arrange

            #region Act
            var (_, _, actualCreateArticleCategories, _) = sortArticlesService.SortArticles(
                articles: articles,
                savedArticles: savedArticles
            );
            #endregion Act

            #region Assert
            var actualCreatedArticlesCount = actualCreateArticleCategories.Count();
            Assert.Equal(
                expected: expectedCreatedArticleCategoriesCount,
                actual: actualCreatedArticlesCount
            );
            #endregion Assert
        }

        [Fact]
        public void SortArticles_ReturnsUpdateArticlesWithSingleArticle_WhenArticlesCollectionHasSingleArticleAndSavedArticlesCollectionHasArticleWithSameNewsPortalIdAndUrl()
        {
            #region Arrange
            var articles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    url: "https://www.google.com",
                    title: "title1",
                    summary: "summary1"
                )
            };
            var savedArticles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    url: "https://www.google.com",
                    title: "title2",
                    summary: "summary2"
                )
            }.ToDictionary(article => article.Id);

            var expectedUpdatedArticlesCount = 1;

            var sortArticlesService = new SortArticlesService();
            #endregion Arrange

            #region Act
            var (_, actualUpdatedArticles, _, _) = sortArticlesService.SortArticles(
                articles: articles,
                savedArticles: savedArticles
            );
            #endregion Act

            #region Assert
            var actualCreatedArticlesCount = actualUpdatedArticles.Count();
            Assert.Equal(
                expected: expectedUpdatedArticlesCount,
                actual: actualCreatedArticlesCount
            );
            #endregion Assert
        }

        [Fact]
        public void SortArticles_ReturnsUpdateArticlesWithSingleArticle_WhenArticlesCollectionHasSingleArticleAndSavedArticlesCollectionHasArticleWithSameNewsPortalIdAndTitle()
        {
            #region Arrange
            var articles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    url: "url1",
                    title: "title1",
                    summary: "summary1"
                )
            };
            var savedArticles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    url: "url2",
                    title: "title1",
                    summary: "summary2"
                )
            }.ToDictionary(article => article.Id);

            var expectedUpdatedArticlesCount = 1;

            var sortArticlesService = new SortArticlesService();
            #endregion Arrange

            #region Act
            var (_, actualUpdatedArticles, _, _) = sortArticlesService.SortArticles(
                articles: articles,
                savedArticles: savedArticles
            );
            #endregion Act

            #region Assert
            var actualCreatedArticlesCount = actualUpdatedArticles.Count();
            Assert.Equal(
                expected: expectedUpdatedArticlesCount,
                actual: actualCreatedArticlesCount
            );
            #endregion Assert
        }

        [Fact]
        public void SortArticles_ReturnsUpdateArticlesWithSingleArticle_WhenArticlesCollectionHasSingleArticleAndSavedArticlesCollectionHasArticleWithSameNewsPortalIdAndSummary()
        {
            #region Arrange
            var articles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    url: "url1",
                    title: "title1",
                    summary: "summary1"
                )
            };
            var savedArticles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    url: "url2",
                    title: "title2",
                    summary: "summary1"
                )
            }.ToDictionary(article => article.Id);

            var expectedUpdatedArticlesCount = 1;

            var sortArticlesService = new SortArticlesService();
            #endregion Arrange

            #region Act
            var (_, actualUpdatedArticles, _, _) = sortArticlesService.SortArticles(
                articles: articles,
                savedArticles: savedArticles
            );
            #endregion Act

            #region Assert
            var actualCreatedArticlesCount = actualUpdatedArticles.Count();
            Assert.Equal(
                expected: expectedUpdatedArticlesCount,
                actual: actualCreatedArticlesCount
            );
            #endregion Assert
        }
        #endregion SortArticles

        #region RemoveDuplicateArticles
        [Fact]
        public void RemoveDuplicateArticles_ReturnsListWithUnchangedNumberOfArticles_WhenThereAreNoDuplicateArticlesPresent()
        {
            #region Arrange
            var articles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    url: "url1",
                    title: "title1",
                    summary: "summary1",
                    id: Guid.NewGuid()
                ),
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    url: "url2",
                    title: "title2",
                    summary: "summary2",
                    id: Guid.NewGuid()
                ),
            };
            var expectedArticlesCount = articles.Count;

            var sortArticlesService = new SortArticlesService();
            #endregion Arrange

            #region Act
            var actualArticles = sortArticlesService.RemoveDuplicateArticles(articles: articles);
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
        public void RemoveDuplicateArticles_ReturnsListWithTwoArticlesRemoved_WhenThereAreTwoDuplicateArticlesPresent()
        {
            #region Arrange
            var articles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    url: "url1",
                    title: "title1",
                    summary: "summary1",
                    id: Guid.NewGuid()
                ),
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    url: "url2",
                    title: "title2",
                    summary: "summary2",
                    id: Guid.NewGuid()
                ),
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    url: "url1",
                    title: "title1",
                    summary: "summary1",
                    id: Guid.NewGuid()
                ),
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    url: "url2",
                    title: "title2",
                    summary: "summary2",
                    id: Guid.NewGuid()
                ),
            };
            var expectedArticlesCount = articles.Count - 2;

            var sortArticlesService = new SortArticlesService();
            #endregion Arrange

            #region Act
            var actualArticles = sortArticlesService.RemoveDuplicateArticles(articles: articles);
            #endregion Act

            #region Assert
            var actualArticlesCount = actualArticles.Count();
            Assert.Equal(
                expected: expectedArticlesCount,
                actual: actualArticlesCount
            );
            #endregion Assert
        }
        #endregion RemoveDuplicateArticles
    }
}

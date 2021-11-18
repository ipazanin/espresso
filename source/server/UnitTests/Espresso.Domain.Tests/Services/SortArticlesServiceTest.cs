// SortArticlesServiceTest.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.Services;
using Espresso.Domain.Tests.TestUtilities;
using Xunit;

namespace Espresso.Domain.Tests.Services
{
    /// <summary>
    /// SortArticlesServiceTest.
    /// </summary>
    public class SortArticlesServiceTest
    {
        [Fact]
        public void SortArticles_ReturnsEmptyCreateArticles_WhenArticlesCollectionIsEmpty()
        {
            var articles = new List<Article>();
            var savedArticles = new List<Article>()
                .ToDictionary(article => article.Id);

            var expectedCreatedArticles = new List<Article>();

            var sortArticlesService = new SortArticlesService();

            var (actualCreatedArticles, _, _, _) = sortArticlesService.SortArticles(
                articles: articles,
                savedArticles: savedArticles);

            Assert.Equal(
                expected: expectedCreatedArticles,
                actual: actualCreatedArticles);
        }

        [Fact]
        public void SortArticles_ReturnsEmptyUpdateArticles_WhenArticlesCollectionIsEmpty()
        {
            var articles = new List<Article>();
            var savedArticles = new List<Article>()
                .ToDictionary(article => article.Id);

            var expectedUpdateArticles = new List<Article>();

            var sortArticlesService = new SortArticlesService();

            var (_, actualUpdateArticlesWithModifiedProperties, _, _) = sortArticlesService.SortArticles(
                articles: articles,
                savedArticles: savedArticles);

            var actualUpdateArticles = actualUpdateArticlesWithModifiedProperties.Select(aricleWithProperties => aricleWithProperties.article);

            Assert.Equal(
                expected: expectedUpdateArticles,
                actual: actualUpdateArticles);
        }

        [Fact]
        public void SortArticles_ReturnsEmptyCreateArticleCategories_WhenArticlesCollectionIsEmpty()
        {
            var articles = new List<Article>();
            var savedArticles = new List<Article>()
                .ToDictionary(article => article.Id);

            var expectedCreatedArticleCategories = new List<ArticleCategory>();

            var sortArticlesService = new SortArticlesService();

            var (_, _, actualCreatedArticleCategories, _) = sortArticlesService.SortArticles(
                articles: articles,
                savedArticles: savedArticles);

            Assert.Equal(
                expected: expectedCreatedArticleCategories,
                actual: actualCreatedArticleCategories);
        }

        [Fact]
        public void SortArticles_ReturnsEmptyUpdateArticleCategories_WhenArticlesCollectionIsEmpty()
        {
            var articles = new List<Article>();
            var savedArticles = new List<Article>()
                .ToDictionary(article => article.Id);

            var expectedUpdateArticleCategories = new List<ArticleCategory>();

            var sortArticlesService = new SortArticlesService();

            var (_, _, _, actualUpdateArticleCategories) = sortArticlesService.SortArticles(
                articles: articles,
                savedArticles: savedArticles);

            Assert.Equal(
                expected: expectedUpdateArticleCategories,
                actual: actualUpdateArticleCategories);
        }

        [Fact]
        public void SortArticles_ReturnsCreateArticlesWithSingleArticle_WhenArticlesCollectionHasSingleArticleAndSavedArticlesCollectionIsEmpty()
        {
            var articles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(default, default!, default!, default!),
            };
            var savedArticles = new List<Article>()
                .ToDictionary(article => article.Id);

            const int ExpectedCreatedArticlesCount = 1;

            var sortArticlesService = new SortArticlesService();

            var (actualCreatedArticles, _, _, _) = sortArticlesService.SortArticles(
                articles: articles,
                savedArticles: savedArticles);

            var actualCreatedArticlesCount = actualCreatedArticles.Count();
            Assert.Equal(
                expected: ExpectedCreatedArticlesCount,
                actual: actualCreatedArticlesCount);
        }

        [Fact]
        public void SortArticles_ReturnsUpdateArticlesWithSingleArticle_WhenArticlesCollectionHasSingleArticleAndSavedArticlesCollectionHasArticleWithSameNewsPortalIdAndUrl()
        {
            var articles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    articleUrl: "https://www.google.com",
                    title: "title1",
                    summary: "summary1"),
            };
            var savedArticles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    articleUrl: "https://www.google.com",
                    title: "title2",
                    summary: "summary2"),
            }.ToDictionary(article => article.Id);

            const int ExpectedUpdatedArticlesCount = 1;

            var sortArticlesService = new SortArticlesService();

            var (_, actualUpdatedArticles, _, _) = sortArticlesService.SortArticles(
                articles: articles,
                savedArticles: savedArticles);

            var actualCreatedArticlesCount = actualUpdatedArticles.Count();
            Assert.Equal(
                expected: ExpectedUpdatedArticlesCount,
                actual: actualCreatedArticlesCount);
        }

        [Fact]
        public void SortArticles_ReturnsUpdateArticlesWithSingleArticle_WhenArticlesCollectionHasSingleArticleAndSavedArticlesCollectionHasArticleWithSameNewsPortalIdAndTitle()
        {
            var articles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    articleUrl: "url1",
                    title: "title1",
                    summary: "summary1"),
            };
            var savedArticles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    articleUrl: "url2",
                    title: "title1",
                    summary: "summary2"),
            }.ToDictionary(article => article.Id);

            const int ExpectedUpdatedArticlesCount = 1;

            var sortArticlesService = new SortArticlesService();

            var (_, actualUpdatedArticles, _, _) = sortArticlesService.SortArticles(
                articles: articles,
                savedArticles: savedArticles);

            var actualCreatedArticlesCount = actualUpdatedArticles.Count();
            Assert.Equal(
                expected: ExpectedUpdatedArticlesCount,
                actual: actualCreatedArticlesCount);
        }

        [Fact]
        public void SortArticles_ReturnsUpdateArticlesWithSingleArticle_WhenArticlesCollectionHasSingleArticleAndSavedArticlesCollectionHasArticleWithSameNewsPortalIdAndSummary()
        {
            var articles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    articleUrl: "url1",
                    title: "title1",
                    summary: "summary1"),
            };
            var savedArticles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    articleUrl: "url2",
                    title: "title2",
                    summary: "summary1"),
            }.ToDictionary(article => article.Id);

            const int ExpectedUpdatedArticlesCount = 1;

            var sortArticlesService = new SortArticlesService();

            var (_, actualUpdatedArticles, _, _) = sortArticlesService.SortArticles(
                articles: articles,
                savedArticles: savedArticles);

            var actualCreatedArticlesCount = actualUpdatedArticles.Count();
            Assert.Equal(
                expected: ExpectedUpdatedArticlesCount,
                actual: actualCreatedArticlesCount);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task RemoveDuplicateArticles_ReturnsListWithUnchangedNumberOfArticles_WhenThereAreNoDuplicateArticlesPresent()
        {
            var articles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    articleUrl: "url1",
                    title: "title1",
                    summary: "summary1",
                    id: Guid.NewGuid()),
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    articleUrl: "url2",
                    title: "title2",
                    summary: "summary2",
                    id: Guid.NewGuid()),
            };
            var expectedArticlesCount = articles.Count;

            var channel = Channel.CreateUnbounded<Article>();
            var writer = channel.Writer;

            articles.ForEach(article => _ = writer.TryWrite(article));
            writer.Complete();

            var sortArticlesService = new SortArticlesService();

            var actualArticles = await sortArticlesService.RemoveDuplicateArticles(channel, default);

            var actualArticlesCount = actualArticles.Count();
            Assert.Equal(
                expected: expectedArticlesCount,
                actual: actualArticlesCount);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task RemoveDuplicateArticles_ReturnsListWithTwoArticlesRemoved_WhenThereAreTwoDuplicateArticlesPresent()
        {
            var articles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    articleUrl: "url1",
                    title: "title1",
                    summary: "summary1",
                    id: Guid.NewGuid()),
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    articleUrl: "url2",
                    title: "title2",
                    summary: "summary2",
                    id: Guid.NewGuid()),
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    articleUrl: "url1",
                    title: "title1",
                    summary: "summary1",
                    id: Guid.NewGuid()),
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    articleUrl: "url2",
                    title: "title2",
                    summary: "summary2",
                    id: Guid.NewGuid()),
            };
            var expectedArticlesCount = articles.Count - 2;
            var channel = Channel.CreateUnbounded<Article>();
            var writer = channel.Writer;

            articles.ForEach(article => _ = writer.TryWrite(article));
            writer.Complete();

            var sortArticlesService = new SortArticlesService();

            var actualArticles = await sortArticlesService.RemoveDuplicateArticles(channel, default);

            var actualArticlesCount = actualArticles.Count();
            Assert.Equal(
                expected: expectedArticlesCount,
                actual: actualArticlesCount);
        }
    }
}

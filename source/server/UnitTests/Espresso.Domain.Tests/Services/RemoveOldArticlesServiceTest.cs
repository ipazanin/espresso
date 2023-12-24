// RemoveOldArticlesServiceTest.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.Infrastructure;
using Espresso.Domain.Services;
using Espresso.Domain.Tests.TestUtilities;
using Espresso.Domain.ValueObjects.SettingsValueObjects;
using Moq;
using Xunit;

namespace Espresso.Domain.Tests.Services;

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
                    publishDateTime: DateTimeOffset.UtcNow),
            };
        var expectedArticlesCount = articles.Count;

        var articleSetting = new ArticleSetting(
                maxAgeOfTrendingArticleInMiliseconds: default,
                maxAgeOfFeaturedArticleInMiliseconds: default,
                maxAgeOfArticleInMiliseconds: (long)TimeSpan.FromHours(2).TotalMilliseconds,
                featuredArticlesTake: default);
        var setting = new Setting(
                id: default,
                settingsRevision: default,
                created: default,
                articleSetting: articleSetting,
                newsPortalSetting: default!,
                jobsSetting: default!,
                similarArticleSetting: default!);

        var settingProviderMock = new Mock<ISettingProvider>(MockBehavior.Strict);
        _ = settingProviderMock.SetupGet(settingProvider => settingProvider.LatestSetting)
            .Returns(setting);

        var sortArticlesService = new RemoveOldArticlesService(settingProviderMock.Object);

        var actualArticles = sortArticlesService.RemoveOldArticles(articles: articles);

        var actualArticlesCount = actualArticles.Count;
        Assert.Equal(
            expected: expectedArticlesCount,
            actual: actualArticlesCount);
    }

    [Fact]
    public void RemoveOldArticles_ReturnsOneLessArticle_WhenOneArticleIsOlderThanProvidedPublishDateTimeThreshold()
    {
        var articles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    publishDateTime: DateTimeOffset.UtcNow.AddHours(-2)),
            };
        var expectedArticlesCount = articles.Count - 1;

        var articleSetting = new ArticleSetting(
                maxAgeOfTrendingArticleInMiliseconds: default,
                maxAgeOfFeaturedArticleInMiliseconds: default,
                maxAgeOfArticleInMiliseconds: (long)TimeSpan.FromHours(2).TotalMilliseconds,
                featuredArticlesTake: default);
        var setting = new Setting(
                id: default,
                settingsRevision: default,
                created: default,
                articleSetting: articleSetting,
                newsPortalSetting: default!,
                jobsSetting: default!,
                similarArticleSetting: default!);

        var settingProviderMock = new Mock<ISettingProvider>(MockBehavior.Strict);
        _ = settingProviderMock.SetupGet(settingProvider => settingProvider.LatestSetting)
            .Returns(setting);

        var sortArticlesService = new RemoveOldArticlesService(settingProviderMock.Object);

        var actualArticles = sortArticlesService.RemoveOldArticles(articles: articles);

        var actualArticlesCount = actualArticles.Count;
        Assert.Equal(
            expected: expectedArticlesCount,
            actual: actualArticlesCount);
    }
}

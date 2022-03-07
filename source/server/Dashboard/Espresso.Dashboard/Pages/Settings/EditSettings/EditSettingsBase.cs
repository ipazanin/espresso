// EditSettingsBase.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Dashboard.Application.IServices;
using Espresso.Dashboard.Pages.Settings.EditSettings.State;
using Espresso.Domain.Entities;
using Espresso.Domain.Infrastructure;
using Espresso.Domain.ValueObjects.SettingsValueObjects;
using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.Settings.EditSettings;

public class EditSettingsBase : ComponentBase
{
    private readonly IDictionary<string, bool> _formValidationStatusDictionary = new Dictionary<string, bool>();

    [Parameter]
    [EditorRequired]
    public int Id { get; init; }

    protected SettingsState SettingsState { get; } = new();

    [Inject]
    private ISettingProvider SettingProvider { get; init; } = null!;

    [Inject]
    private ISettingChangedService SettingChangedService { get; init; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; init; } = null!;

    protected override void OnInitialized()
    {
        SettingsState.ArticleSettingState.FeaturedArticlesTake = SettingProvider.LatestSetting.ArticleSetting.FeaturedArticlesTake;
        SettingsState.ArticleSettingState.MaxAgeOfArticleInDays = (int)SettingProvider.LatestSetting.ArticleSetting.MaxAgeOfArticle.TotalDays;
        SettingsState.ArticleSettingState.MaxAgeOfFeaturedArticleInHours = (int)SettingProvider.LatestSetting.ArticleSetting.MaxAgeOfFeaturedArticle.TotalHours;
        SettingsState.ArticleSettingState.MaxAgeOfTrendingArticleInHours = (int)SettingProvider.LatestSetting.ArticleSetting.MaxAgeOfTrendingArticle.TotalHours;

        SettingsState.JobsSettingState.AnalyticsCronExpression = SettingProvider.LatestSetting.JobsSetting.AnalyticsCronExpression;
        SettingsState.JobsSettingState.ParseArticlesCronExpression = SettingProvider.LatestSetting.JobsSetting.ParseArticlesCronExpression;
        SettingsState.JobsSettingState.WebApiReportCronExpression = SettingProvider.LatestSetting.JobsSetting.WebApiReportCronExpression;

        SettingsState.NewsPortalSettingState.MaxAgeOfNewNewsPortalInDays = (int)SettingProvider.LatestSetting.NewsPortalSetting.MaxAgeOfNewNewsPortal.TotalDays;
        SettingsState.NewsPortalSettingState.NewNewsPortalsPositionInApp = SettingProvider.LatestSetting.NewsPortalSetting.NewNewsPortalsPosition;

        SettingsState.SimilarArticlesState.MaxArticleAgeToParseInSimilarArticlesInHours = (int)SettingProvider.LatestSetting.SimilarArticleSetting.MaxAgeOfSimilarArticleChecking.TotalHours;
        SettingsState.SimilarArticlesState.MaxDurationBetweenTwoSimilarArticlesInHours = (int)SettingProvider.LatestSetting.SimilarArticleSetting.ArticlePublishDateTimeDifferenceThreshold.TotalHours;
        SettingsState.SimilarArticlesState.MinimalNumberOfWordsRequirement = SettingProvider.LatestSetting.SimilarArticleSetting.MinimalNumberOfWordsForArticleToBeComparable;
        SettingsState.SimilarArticlesState.SimilarityScoreThreshold = SettingProvider.LatestSetting.SimilarArticleSetting.SimilarityScoreThreshold;

        StateHasChanged();
    }

    protected async Task OnSubmit()
    {
        if (_formValidationStatusDictionary.Values.Any(value => !value))
        {
            return;
        }

        var newArticleSetting = new ArticleSetting(
            maxAgeOfTrendingArticleInMiliseconds: (long)TimeSpan.FromHours(SettingsState.ArticleSettingState.MaxAgeOfTrendingArticleInHours).TotalMilliseconds,
            maxAgeOfFeaturedArticleInMiliseconds: (long)TimeSpan.FromHours(SettingsState.ArticleSettingState.MaxAgeOfFeaturedArticleInHours).TotalMilliseconds,
            maxAgeOfArticleInMiliseconds: (long)TimeSpan.FromDays(SettingsState.ArticleSettingState.MaxAgeOfArticleInDays).TotalMilliseconds,
            featuredArticlesTake: SettingsState.ArticleSettingState.FeaturedArticlesTake);

        var newNewsPortalSettings = new NewsPortalSetting(
            maxAgeOfNewNewsPortalInMiliseconds: (long)TimeSpan.FromDays(SettingsState.NewsPortalSettingState.MaxAgeOfNewNewsPortalInDays).TotalMilliseconds,
            newNewsPortalsPosition: SettingsState.NewsPortalSettingState.NewNewsPortalsPositionInApp);

        var newJobsSettings = new JobsSetting(
            analyticsCronExpression: SettingsState.JobsSettingState.AnalyticsCronExpression,
            webApiReportCronExpression: SettingsState.JobsSettingState.WebApiReportCronExpression,
            parseArticlesCronExpression: SettingsState.JobsSettingState.ParseArticlesCronExpression);

        var newSimilarArticleSetting = new SimilarArticleSetting(
            similarityScoreThreshold: SettingsState.SimilarArticlesState.SimilarityScoreThreshold,
            articlePublishDateTimeDifferenceThresholdInMiliseconds: (long)TimeSpan.FromHours(SettingsState.SimilarArticlesState.MaxDurationBetweenTwoSimilarArticlesInHours).TotalMilliseconds,
            maxAgeOfSimilarArticleCheckingInMiliseconds: (long)TimeSpan.FromHours(SettingsState.SimilarArticlesState.MaxArticleAgeToParseInSimilarArticlesInHours).TotalMilliseconds,
            minimalNumberOfWordsForArticleToBeComparable: SettingsState.SimilarArticlesState.MinimalNumberOfWordsRequirement);

        var newSetting = new Setting(
            id: default,
            settingsRevision: default,
            created: DateTimeOffset.UtcNow,
            articleSetting: newArticleSetting,
            newsPortalSetting: newNewsPortalSettings,
            jobsSetting: newJobsSettings,
            similarArticleSetting: newSimilarArticleSetting);

        await SettingChangedService.UpdateSetting(newSetting, default);

        NavigationManager.NavigateTo(uri: "/settings");
    }

    protected void OnIsValidChanged((bool isValid, string uniqueId) changeArgs)
    {
        var (value, uniqueId) = changeArgs;

        _formValidationStatusDictionary[uniqueId] = value;
    }
}

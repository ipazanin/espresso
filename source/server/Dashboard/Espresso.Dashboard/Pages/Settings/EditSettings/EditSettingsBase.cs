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

        StateHasChanged();
    }

    protected async Task OnSubmit()
    {
        if (_formValidationStatusDictionary.Values.Any(value => !value))
        {
            return;
        }

        Console.WriteLine(SettingsState.ArticleSettingState.FeaturedArticlesTake);

        var setting = SettingProvider.LatestSetting;

        var newArticleSetting = new ArticleSetting(
            maxAgeOfTrendingArticleInMiliseconds: (long)TimeSpan.FromHours(SettingsState.ArticleSettingState.MaxAgeOfTrendingArticleInHours).TotalMilliseconds,
            maxAgeOfFeaturedArticleInMiliseconds: (long)TimeSpan.FromHours(SettingsState.ArticleSettingState.MaxAgeOfFeaturedArticleInHours).TotalMilliseconds,
            maxAgeOfArticleInMiliseconds: (long)TimeSpan.FromDays(SettingsState.ArticleSettingState.MaxAgeOfArticleInDays).TotalMilliseconds,
            featuredArticlesTake: SettingsState.ArticleSettingState.FeaturedArticlesTake);

        var newNewsPortalSettings = new NewsPortalSetting(
            maxAgeOfNewNewsPortalInMiliseconds: setting.NewsPortalSetting.MaxAgeOfNewNewsPortalInMiliseconds,
            newNewsPortalsPosition: setting.NewsPortalSetting.NewNewsPortalsPosition);

        var newJobsSettings = new JobsSetting(
            analyticsCronExpression: SettingsState.JobsSettingState.AnalyticsCronExpression,
            webApiReportCronExpression: SettingsState.JobsSettingState.WebApiReportCronExpression,
            parseArticlesCronExpression: SettingsState.JobsSettingState.ParseArticlesCronExpression);

        var newSimilarArticleSetting = new SimilarArticleSetting(
            similarityScoreThreshold: setting.SimilarArticleSetting.SimilarityScoreThreshold,
            articlePublishDateTimeDifferenceThresholdInMiliseconds: setting.SimilarArticleSetting.ArticlePublishDateTimeDifferenceThresholdInMiliseconds,
            maxAgeOfSimilarArticleCheckingInMiliseconds: setting.SimilarArticleSetting.MaxAgeOfSimilarArticleCheckingInMiliseconds,
            minimalNumberOfWordsForArticleToBeComparable: setting.SimilarArticleSetting.MinimalNumberOfWordsForArticleToBeComparable);

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

// SettingProvider.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.Infrastructure;
using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Application.Infrastructure.SettingsInfrastructure;

/// <summary>
/// <see cref="Setting"/> provider.
/// </summary>
public sealed class SettingProvider : ISettingProvider
{
    private static readonly Setting s_defaultSetting = new(
        id: 1,
        settingsRevision: 1,
        created: DateTimeOffset.UtcNow,
        articleSetting: new Domain.ValueObjects.SettingsValueObjects.ArticleSetting(
            maxAgeOfTrendingArticleInMiliseconds: (long)TimeSpan.FromHours(2).TotalMilliseconds,
            maxAgeOfFeaturedArticleInMiliseconds: (long)TimeSpan.FromHours(2).TotalMilliseconds,
            maxAgeOfArticleInMiliseconds: (long)TimeSpan.FromDays(5).TotalMilliseconds,
            featuredArticlesTake: 10),
        newsPortalSetting: new Domain.ValueObjects.SettingsValueObjects.NewsPortalSetting(
            maxAgeOfNewNewsPortalInMiliseconds: (long)TimeSpan.FromDays(60).TotalMilliseconds,
            newNewsPortalsPosition: 3),
        jobsSetting: new Domain.ValueObjects.SettingsValueObjects.JobsSetting(
            analyticsCronExpression: "1 16 * * *",
            webApiReportCronExpression: "0 9 * * *",
            parseArticlesCronExpression: "* * * * *"),
        similarArticleSetting: new Domain.ValueObjects.SettingsValueObjects.SimilarArticleSetting(
            similarityScoreThreshold: 0.65,
            articlePublishDateTimeDifferenceThresholdInMiliseconds: (long)TimeSpan.FromHours(24).TotalMilliseconds,
            maxAgeOfSimilarArticleCheckingInMiliseconds: (long)TimeSpan.FromHours(26).TotalMilliseconds,
            minimalNumberOfWordsForArticleToBeComparable: 4));

    private readonly IEspressoDatabaseContext _espressoDatabaseContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingProvider"/> class.
    /// </summary>
    /// <param name="espressoDatabaseContext">Espresso database context.</param>
    public SettingProvider(IEspressoDatabaseContext espressoDatabaseContext)
    {
        _espressoDatabaseContext = espressoDatabaseContext;

        LatestSetting = GetLatestSettingFromStore();
    }

    /// <inheritdoc/>
    public Setting LatestSetting { get; private set; }

    /// <inheritdoc/>
    public async Task UpdateLatestSetting(CancellationToken cancellationToken)
    {
        LatestSetting = await GetLatestSettingFromStoreAsync(cancellationToken);
    }

    private Setting GetLatestSettingFromStore()
    {
        _espressoDatabaseContext.Database.Migrate();

        var latestSetting = _espressoDatabaseContext
            .Settings
            .OrderByDescending(setting => setting.Created)
            .FirstOrDefault();

        return latestSetting ?? s_defaultSetting;
    }

    private Task<Setting> GetLatestSettingFromStoreAsync(CancellationToken cancellationToken)
    {
        return _espressoDatabaseContext
            .Settings
            .OrderByDescending(setting => setting.Created)
            .FirstAsync(cancellationToken);
    }
}

// SettingDto.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.DataTransferObjects.SettingDataTransferObjects;

public class SettingDto
{
    public SettingDto(
        int id,
        int revision,
        DateTimeOffset created,
        JobsSettingDto jobsSetting,
        ArticleSettingDto articleSetting,
        SimilarArticlesSettingDto similarArticlesSetting,
        NewsPortalSettingDto newsPortalSetting)
    {
        Id = id;
        Revision = revision;
        Created = created;
        JobsSetting = jobsSetting;
        ArticleSetting = articleSetting;
        SimilarArticlesSetting = similarArticlesSetting;
        NewsPortalSetting = newsPortalSetting;
    }

    public static Expression<Func<Setting, SettingDto>> Projection
    {
        get => setting => new SettingDto(
            setting.Id,
            setting.SettingsRevision,
            setting.Created,
            JobsSettingDto.Projection.Compile().Invoke(setting.JobsSetting),
            ArticleSettingDto.Projection.Compile().Invoke(setting.ArticleSetting),
            SimilarArticlesSettingDto.Projection.Compile().Invoke(setting.SimilarArticleSetting),
            NewsPortalSettingDto.Projection.Compile().Invoke(setting.NewsPortalSetting));
    }

    public int Id { get; }

    public int Revision { get; }

    public DateTimeOffset Created { get; }

    public JobsSettingDto JobsSetting { get; }

    public ArticleSettingDto ArticleSetting { get; }

    public SimilarArticlesSettingDto SimilarArticlesSetting { get; }

    public NewsPortalSettingDto NewsPortalSetting { get; }

    public Setting CreateSetting()
    {
        return new Setting(
            id: default,
            settingsRevision: Revision + 1,
            created: DateTimeOffset.UtcNow,
            articleSetting: ArticleSetting.CreateArticleSetting(),
            newsPortalSetting: NewsPortalSetting.CreateNewsPortalSetting(),
            jobsSetting: JobsSetting.CreateJobsSetting(),
            similarArticleSetting: SimilarArticlesSetting.CreateSimilarArticleSetting());
    }
}

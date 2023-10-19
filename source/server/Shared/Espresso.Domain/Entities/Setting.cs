// Setting.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Infrastructure;
using Espresso.Domain.ValueObjects.SettingsValueObjects;

namespace Espresso.Domain.Entities;

/// <summary>
/// Represents application settings.
/// </summary>
public class Setting : IEntity<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Setting"/> class.
    /// </summary>
    /// <param name="id">Id.</param>
    public Setting(int id)
    {
        Id = id;
        Created = DateTimeOffset.UtcNow;
        ArticleSetting = null!;
        NewsPortalSetting = null!;
        JobsSetting = null!;
        SimilarArticleSetting = null!;
    }

    public Setting(
        int id,
        int settingsRevision,
        DateTimeOffset created,
        ArticleSetting articleSetting,
        NewsPortalSetting newsPortalSetting,
        JobsSetting jobsSetting,
        SimilarArticleSetting similarArticleSetting)
        : this(id)
    {
        SettingsRevision = settingsRevision;
        Created = created;
        ArticleSetting = articleSetting;
        NewsPortalSetting = newsPortalSetting;
        JobsSetting = jobsSetting;
        SimilarArticleSetting = similarArticleSetting;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Setting"/> class.
    /// ORM Constructor.
    /// </summary>
    private Setting()
    {
        ArticleSetting = null!;
        NewsPortalSetting = null!;
        JobsSetting = null!;
        SimilarArticleSetting = null!;
    }

#pragma warning disable RCS1170 // Use read-only auto-implemented property.
    /// <summary>
    /// Gets id.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets revision of <see cref="Setting"/>.
    /// </summary>
    public int SettingsRevision { get; private set; }

    /// <summary>
    /// Gets time when this revision was created.
    /// </summary>
    public DateTimeOffset Created { get; private set; }

    /// <summary>
    /// Gets <see cref="Article"/> settings.
    /// </summary>
    public ArticleSetting ArticleSetting { get; private set; }

    /// <summary>
    /// Gets <see cref="NewsPortal"/> settings.
    /// </summary>
    public NewsPortalSetting NewsPortalSetting { get; private set; }

    /// <summary>
    /// Gets cron jobs setting.
    /// </summary>
    public JobsSetting JobsSetting { get; private set; }

    /// <summary>
    /// Gets <see cref="SimilarArticle"/> setting.
    /// </summary>
    public SimilarArticleSetting SimilarArticleSetting { get; private set; }

#pragma warning restore RCS1170 // Use read-only auto-implemented property.
}

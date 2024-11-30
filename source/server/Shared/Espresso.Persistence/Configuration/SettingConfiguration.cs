// SettingConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration;

/// <summary>
/// <see cref="Setting"/> entity configuration.
/// </summary>
public class SettingConfiguration : IEntityTypeConfiguration<Setting>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Setting> builder)
    {
        _ = builder.Property(setting => setting.SettingsRevision)
            .ValueGeneratedOnAdd();

        _ = builder.HasIndex(setting => setting.Created);

        ConfigureArticleSetting(builder);
        ConfigureNewsPortalSetting(builder);
        ConfigureJobsSetting(builder);
        ConfigureSimilarArticleSetting(builder);
    }

    private static void ConfigureArticleSetting(EntityTypeBuilder<Setting> builder)
    {
        _ = builder.OwnsOne(setting => setting.ArticleSetting);
    }

    private static void ConfigureNewsPortalSetting(EntityTypeBuilder<Setting> builder)
    {
        _ = builder.OwnsOne(setting => setting.NewsPortalSetting);
    }

    private static void ConfigureJobsSetting(EntityTypeBuilder<Setting> builder)
    {
        _ = builder.OwnsOne(setting => setting.JobsSetting);
    }

    private static void ConfigureSimilarArticleSetting(EntityTypeBuilder<Setting> builder)
    {
        _ = builder.OwnsOne(setting => setting.SimilarArticleSetting);
    }
}

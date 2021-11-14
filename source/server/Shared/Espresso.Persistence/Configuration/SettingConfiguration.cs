// SettingConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Espresso.Persistence.Configuration
{
    /// <summary>
    /// <see cref="Setting"/> entity configuration.
    /// </summary>
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        private const int SettingId = 1;

        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.Property(setting => setting.SettingsRevision)
                .ValueGeneratedOnAdd();

            builder.HasIndex(setting => setting.Created);

            // builder.HasData(new[] { new Setting(SettingId) });

            ConfigureArticleSetting(builder);
            ConfigureNewsPortalSetting(builder);
            ConfigureJobsSetting(builder);
            ConfigureSimilarArticleSetting(builder);
        }

        private static void ConfigureArticleSetting(EntityTypeBuilder<Setting> builder)
        {
            var articleSettingBuilder = builder.OwnsOne(setting => setting.ArticleSetting);

            //articleSettingBuilder
            //    .HasData(new[]
            //    {
            //        new
            //        {
            //            SettingId,
            //            MaxAgeOfTrendingArticleInMiliseconds = (long)TimeSpan.FromHours(6).TotalMilliseconds,
            //            MaxAgeOfFeaturedArticleInMiliseconds = (long)TimeSpan.FromHours(6).TotalMilliseconds,
            //            MaxAgeOfArticleInMiliseconds = (long)TimeSpan.FromDays(5).TotalMilliseconds,
            //            FeaturedArticlesTake = 10,
            //        },
            //    });
        }

        private static void ConfigureNewsPortalSetting(EntityTypeBuilder<Setting> builder)
        {
            var newsPortalSettingBuilder = builder.OwnsOne(setting => setting.NewsPortalSetting);

            //newsPortalSettingBuilder
            //    .HasData(new[]
            //    {
            //        new
            //        {
            //            SettingId,
            //            MaxAgeOfNewNewsPortalInMiliseconds = (long)TimeSpan.FromDays(60).TotalMilliseconds,
            //            NewNewsPortalsPosition = 3,
            //        },
            //    });
        }

        private static void ConfigureJobsSetting(EntityTypeBuilder<Setting> builder)
        {
            var jobsSettingBuilder = builder.OwnsOne(setting => setting.JobsSetting);

            //jobsSettingBuilder
            //    .HasData(new[]
            //    {
            //        new
            //        {
            //            SettingId,
            //            AnalyticsCronExpression = "0 10 * * *",
            //            WebApiReportCronExpression = "0 9 * * *",
            //            ParseArticlesCronExpression = "*/1 * * * *",
            //        },
            //    });
        }

        private static void ConfigureSimilarArticleSetting(EntityTypeBuilder<Setting> builder)
        {
            var similarArticleSettingBuilder = builder.OwnsOne(setting => setting.SimilarArticleSetting);

            //similarArticleSettingBuilder
            //    .HasData(new[]
            //    {
            //        new
            //        {
            //            SettingId,
            //            SimilarityScoreThreshold = 0.65d,
            //            ArticlePublishDateTimeDifferenceThresholdInMiliseconds = (long)TimeSpan.FromHours(24).TotalMilliseconds,
            //            MaxAgeOfSimilarArticleCheckingInMiliseconds = (long)TimeSpan.FromHours(26).TotalMilliseconds,
            //            MinimalNumberOfWordsForArticleToBeComparable = 4,
            //        },
            //    });
        }
    }
}

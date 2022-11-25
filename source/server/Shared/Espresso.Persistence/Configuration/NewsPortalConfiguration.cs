// NewsPortalConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Persistence.DataSeed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration;

/// <summary>
/// <see cref="NewsPortal"/> entity configuration.
/// </summary>
public class NewsPortalConfiguration : IEntityTypeConfiguration<NewsPortal>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<NewsPortal> builder)
    {
        _ = builder.Property(newsPortal => newsPortal.Name)
            .HasMaxLength(NewsPortal.NameMaxLength);

        _ = builder.Property(newsPortal => newsPortal.BaseUrl)
            .HasMaxLength(NewsPortal.BaseUrlMaxLength);

        _ = builder.Property(newsPortal => newsPortal.IconUrl)
            .HasMaxLength(NewsPortal.IconUrlMaxLength);

        _ = builder.Property(newsPortal => newsPortal.IsEnabled)
            .HasDefaultValue(NewsPortal.IsEnabledDefaultValue);

        _ = builder.HasMany(newsPortal => newsPortal.RssFeeds)
            .WithOne(rssFeed => rssFeed.NewsPortal!)
            .HasForeignKey(rssFeed => rssFeed.NewsPortalId)
            .OnDelete(DeleteBehavior.Restrict);

        _ = builder.HasMany(newsPortal => newsPortal.Articles)
            .WithOne(article => article.NewsPortal!)
            .HasForeignKey(article => article.NewsPortalId)
            .OnDelete(DeleteBehavior.Restrict);

        _ = builder.HasOne(newsPortal => newsPortal.Category)
            .WithMany(category => category!.NewsPortals)
            .HasForeignKey(newsPortal => newsPortal.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        _ = builder.HasOne(newsPortal => newsPortal.Region)
            .WithMany(region => region!.NewsPortals)
            .HasForeignKey(newsPortal => newsPortal.RegionId)
            .OnDelete(DeleteBehavior.Cascade);

        NewsPortalDataSeed.SeedData(builder);
        LocalNewsPortalDataSeed.SeedData(builder);
    }
}

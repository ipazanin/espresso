// RssFeedCategoryConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration;

/// <summary>
/// <see cref="RssFeedCategory"/> entity configuration.
/// </summary>
public class RssFeedCategoryConfiguration : IEntityTypeConfiguration<RssFeedCategory>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<RssFeedCategory> builder)
    {
        _ = builder.Property(rssFeedcategory => rssFeedcategory.UrlRegex)
            .HasMaxLength(RssFeedCategory.UrlRegexMaxLength);

        _ = builder.HasOne(rssFeedCategory => rssFeedCategory.RssFeed)
            .WithMany(rssFeed => rssFeed!.RssFeedCategories)
            .HasForeignKey(rssFeedCategory => rssFeedCategory.RssFeedId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

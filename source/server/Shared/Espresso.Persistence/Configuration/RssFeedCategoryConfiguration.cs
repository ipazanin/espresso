// RssFeedCategoryConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Persistence.DataSeed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration
{
    public class RssFeedCategoryConfiguration : IEntityTypeConfiguration<RssFeedCategory>
    {
        public void Configure(EntityTypeBuilder<RssFeedCategory> builder)
        {
            builder.Property(rssFeedcategory => rssFeedcategory.UrlRegex)
                .HasMaxLength(RssFeedCategory.UrlRegexMaxLength);

            builder.HasOne(rssFeedCategory => rssFeedCategory.RssFeed)
                .WithMany(rssFeed => rssFeed!.RssFeedCategories)
                .HasForeignKey(rssFeedCategory => rssFeedCategory.RssFeedId)
                .OnDelete(DeleteBehavior.Cascade);

            RssFeedCategoryDataSeed.Seed(builder);
        }
    }
}

using Espresso.Common.Constants;
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
            #region Property Mapping
            builder.Property(rssFeedcategory => rssFeedcategory.UrlRegex)
                .HasMaxLength(RssFeedCategory.UrlRegexMaxLength);
            #endregion

            #region Relationship Mapping
            builder.HasOne(rssFeedCategory => rssFeedCategory.RssFeed)
                .WithMany(rssFeed => rssFeed!.RssFeedCategories)
                .HasForeignKey(rssFeedCategory => rssFeedCategory.RssFeedId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Data Seed
            RssFeedCategoryDataSeed.Seed(builder);
            #endregion
        }

    }
}

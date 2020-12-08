using Espresso.Domain.Entities;
using Espresso.Persistence.DataSeed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration
{
    public class RssFeedContentModifierConfiguration : IEntityTypeConfiguration<RssFeedContentModifier>
    {
        public void Configure(EntityTypeBuilder<RssFeedContentModifier> builder)
        {
            #region Properties
            builder.Property(rssfeedContentModifier => rssfeedContentModifier.SourceValue)
                .HasMaxLength(RssFeedContentModifier.SourceValueMaxLength);

            builder.Property(rssfeedContentModifier => rssfeedContentModifier.ReplacementValue)
                .HasMaxLength(RssFeedContentModifier.ReplacementValueMaxLength);
            #endregion

            #region Relationships
            builder.HasOne(rssFeedModifier => rssFeedModifier.RssFeed)
                .WithMany(rssFeed => rssFeed!.RssFeedContentModifiers)
                .HasForeignKey(rssFeedModifier => rssFeedModifier.RssFeedId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            RssFeedContentModifierDataSeed.Seed(builder);
        }
    }
}

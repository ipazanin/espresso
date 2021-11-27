// RssFeedContentModifierConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Persistence.DataSeed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration;

/// <summary>
/// <see cref="RssFeedContentModifier"/> entity configuration.
/// </summary>
public class RssFeedContentModifierConfiguration : IEntityTypeConfiguration<RssFeedContentModifier>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<RssFeedContentModifier> builder)
    {
        builder.Property(rssfeedContentModifier => rssfeedContentModifier.SourceValue)
            .HasMaxLength(RssFeedContentModifier.SourceValueMaxLength);

        builder.Property(rssfeedContentModifier => rssfeedContentModifier.ReplacementValue)
            .HasMaxLength(RssFeedContentModifier.ReplacementValueMaxLength);

        builder.HasOne(rssFeedModifier => rssFeedModifier.RssFeed)
            .WithMany(rssFeed => rssFeed!.RssFeedContentModifiers)
            .HasForeignKey(rssFeedModifier => rssFeedModifier.RssFeedId)
            .OnDelete(DeleteBehavior.Cascade);

        RssFeedContentModifierDataSeed.Seed(builder);
    }
}

// RssFeedContentModifierConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
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
        _ = builder.ToTable(nameof(RssFeedContentModifier));

        _ = builder.Property(rssfeedContentModifier => rssfeedContentModifier.SourceValue)
            .HasMaxLength(RssFeedContentModifier.SourceValueMaxLength);

        _ = builder.Property(rssfeedContentModifier => rssfeedContentModifier.ReplacementValue)
            .HasMaxLength(RssFeedContentModifier.ReplacementValueMaxLength);

        _ = builder.HasOne(rssFeedModifier => rssFeedModifier.RssFeed)
            .WithMany(rssFeed => rssFeed!.RssFeedContentModifiers)
            .HasForeignKey(rssFeedModifier => rssFeedModifier.RssFeedId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

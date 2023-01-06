// NewsPortalImageConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration;

public class NewsPortalImageConfiguration : IEntityTypeConfiguration<NewsPortalImage>
{
    public void Configure(EntityTypeBuilder<NewsPortalImage> builder)
    {
        builder.HasOne(newsPortalImage => newsPortalImage.NewsPortal)
            .WithOne(newsPortal => newsPortal.NewsPortalImage)
            .HasForeignKey<NewsPortalImage>(newsPortalImage => newsPortalImage.NewsPortalId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

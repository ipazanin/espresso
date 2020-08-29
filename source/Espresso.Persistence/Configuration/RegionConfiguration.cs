using System.Collections.Generic;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.RegionEnums;
using Espresso.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.Property(region => region.Name)
                .HasMaxLength(Region.RegionNameHasMaxLength);

            builder.Property(region => region.Subtitle)
                .HasMaxLength(Region.RegionSubtitleHasMaxLength);

            builder.HasMany(region => region.NewsPortals)
                .WithOne(newsPortal => newsPortal.Region!)
                .HasForeignKey(newsPortal => newsPortal.RegionId)
                .OnDelete(DeleteBehavior.Cascade);

            Seed(builder);
        }

        private void Seed(EntityTypeBuilder<Region> builder)
        {
            var regions = new List<Region>
            {
                new Region(
                    id: (int)RegionId.Global,
                    name: RegionId.Global.GetDisplayName(),
                    subtitle: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt"
                ),
                new Region(
                    id: (int)RegionId.Dalmacija,
                    name: RegionId.Dalmacija.GetDisplayName(),
                    subtitle: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt"
                ),
                new Region(
                    id: (int)RegionId.Istra,
                    name: RegionId.Istra.GetDisplayName(),
                    subtitle: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt"
                ),
                new Region(
                    id: (int)RegionId.Lika,
                    name: RegionId.Lika.GetDisplayName(),
                    subtitle: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt"
                ),
                new Region(
                    id: (int)RegionId.SjevernaHrvatska,
                    name: RegionId.SjevernaHrvatska.GetDisplayName(),
                    subtitle: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt"
                ),
                new Region(
                    id: (int)RegionId.Slavonija,
                    name: RegionId.Slavonija.GetDisplayName(),
                    subtitle: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt"
                ),
                new Region(
                    id: (int)RegionId.Zagreb,
                    name: RegionId.Zagreb.GetDisplayName(),
                    subtitle: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt"
                ),
            };

            builder.HasData(regions);
        }
    }
}

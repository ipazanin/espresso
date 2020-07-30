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
                .HasMaxLength(PropertyConstraintConstants.RegionNameHasMaxLength)
                .IsRequired(PropertyConstraintConstants.RegionNameIsRequired);

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
                    name: RegionId.Global.GetDisplayName()
                ),
                new Region(
                    id: (int)RegionId.Dalmacija,
                    name: RegionId.Dalmacija.GetDisplayName()
                ),
                new Region(
                    id: (int)RegionId.Istra,
                    name: RegionId.Istra.GetDisplayName()
                ),
                new Region(
                    id: (int)RegionId.Lika,
                    name: RegionId.Lika.GetDisplayName()
                ),
                new Region(
                    id: (int)RegionId.SjevernaHrvatska,
                    name: RegionId.SjevernaHrvatska.GetDisplayName()
                ),
                new Region(
                    id: (int)RegionId.Slavonija,
                    name: RegionId.Slavonija.GetDisplayName()
                ),
                new Region(
                    id: (int)RegionId.Zagreb,
                    name: RegionId.Zagreb.GetDisplayName()
                ),
            };

            builder.HasData(regions);
        }
    }
}

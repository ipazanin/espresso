// RegionConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Extensions;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.RegionEnums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration;

/// <summary>
/// <see cref="Region"/> entity configuration.
/// </summary>
public class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        _ = builder.Property(region => region.Name)
            .HasMaxLength(Region.RegionNameHasMaxLength);

        _ = builder.Property(region => region.Subtitle)
            .HasMaxLength(Region.RegionSubtitleHasMaxLength);

        _ = builder.HasMany(region => region.NewsPortals)
            .WithOne(newsPortal => newsPortal.Region!)
            .HasForeignKey(newsPortal => newsPortal.RegionId)
            .OnDelete(DeleteBehavior.Cascade);

        Seed(builder);
    }

    private static void Seed(EntityTypeBuilder<Region> builder)
    {
        var regions = new List<Region>
            {
                new(
                    id: (int)RegionId.Global,
                    name: RegionId.Global.GetDisplayName(),
                    subtitle: "Global"),
                new(
                    id: (int)RegionId.Dalmacija,
                    name: RegionId.Dalmacija.GetDisplayName(),
                    subtitle: "Split, Zadar, Dubrovnik, Šibenik, Kaštela, Imotski..."),
                new(
                    id: (int)RegionId.Istra,
                    name: RegionId.Istra.GetDisplayName(),
                    subtitle: "Rijeka, Pula, Opatija, Pazin, Umag, Poreč, Rovinj..."),
                new(
                    id: (int)RegionId.Lika,
                    name: RegionId.Lika.GetDisplayName(),
                    subtitle: "Lokalne vijesti iz Ličko-Senjske županije"),
                new(
                    id: (int)RegionId.SjevernaHrvatska,
                    name: RegionId.SjevernaHrvatska.GetDisplayName(),
                    subtitle: "Međimurje, Podravina, Sisak, Zagorje..."),
                new(
                    id: (int)RegionId.Slavonija,
                    name: RegionId.Slavonija.GetDisplayName(),
                    subtitle: "Osijek, Vinkovci, Slavonski Brod, Vukovar, Požega..."),
                new(
                    id: (int)RegionId.Zagreb,
                    name: RegionId.Zagreb.GetDisplayName(),
                    subtitle: "Lokalne vijesti iz grada Zagreba i okolice"),
            };

        _ = builder.HasData(regions);
    }
}

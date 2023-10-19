// CountryConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration;

public sealed class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        _ = builder.Property(country => country.Name).HasMaxLength(Country.NameMaxLength);

        _ = builder.HasMany(country => country.NewsPortals)
            .WithOne(newsPortal => newsPortal.Country)
            .HasForeignKey(newsPortal => newsPortal.CountryId);

        _ = builder.HasOne(country => country.CountryImage)
            .WithOne(countryImage => countryImage.Country)
            .HasForeignKey<CountryImage>(countryImage => countryImage.CountryId)
            .OnDelete(DeleteBehavior.Cascade);

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<Country> builder)
    {
        var defaultCountry = new Country(
            id: Country.DefaultCountryId,
            name: "Hrvatska");

        _ = builder.HasData(defaultCountry);
    }
}

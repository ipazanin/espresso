// CountryImageConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Espresso.Persistence.Configuration;

public class CountryImageConfiguration : IEntityTypeConfiguration<CountryImage>
{
    public void Configure(EntityTypeBuilder<CountryImage> builder)
    {
        builder.HasOne(countryImage => countryImage.Country)
            .WithOne(country => country.CountryImage)
            .HasForeignKey<CountryImage>(countryImage => countryImage.CountryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

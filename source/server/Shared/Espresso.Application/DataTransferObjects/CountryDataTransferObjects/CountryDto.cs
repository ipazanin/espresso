// CountryDto.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.DataTransferObjects.CountryDataTransferObjects;

public sealed class CountryDto
{
    private CountryDto()
    {
        Name = string.Empty;
        RelativeImageUrl = string.Empty;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public string RelativeImageUrl { get; set; }

    public static Expression<Func<Country, CountryDto>> GetProjection()
    {
        return country => new CountryDto
        {
            Id = country.Id,
            Name = country.Name,
            RelativeImageUrl = country.CountryImage!.RelativeUrl,
        };
    }

    public Country CreateCountry()
    {
        return new Country(
            id: Id,
            name: Name);
    }
}

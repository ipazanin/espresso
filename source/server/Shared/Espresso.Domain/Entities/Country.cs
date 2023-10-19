// Country.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.Entities;

#pragma warning disable RCS1170 // Use read-only auto-implemented property.

public sealed class Country : IEntity<int>
{
    public const int NameMaxLength = 100;

    public const int DefaultCountryId = 1;

    public Country(int id, string name)
    {
        Id = id;
        Name = name;
        NewsPortals = new List<NewsPortal>();
    }

    public Country(int id, string name, CountryImage countryImage, ICollection<NewsPortal> newsPortals)
    {
        Id = id;
        Name = name;
        CountryImage = countryImage;
        NewsPortals = newsPortals;
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public CountryImage? CountryImage { get; private set; }

    public ICollection<NewsPortal> NewsPortals { get; private set; }
}

#pragma warning restore RCS1170 // Use read-only auto-implemented property.

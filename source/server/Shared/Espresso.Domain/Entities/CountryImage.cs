// CountryImage.cs
//
// © 2022 Espresso News. All rights reserved.

#pragma warning disable RCS1170 // Use read-only auto-implemented property.

using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.Entities;

public class CountryImage : IEntity<int>
{
    public CountryImage(
        int id,
        byte[] imageBytes,
        string relativeUrl,
        int countryId)
    {
        Id = id;
        ImageBytes = imageBytes;
        RelativeUrl = relativeUrl;
        CountryId = countryId;
    }

    public int Id { get; private set; }

    public byte[] ImageBytes { get; private set; }

    public string RelativeUrl { get; private set; }

    public int CountryId { get; private set; }

    public Country? Country { get; private set; }
}

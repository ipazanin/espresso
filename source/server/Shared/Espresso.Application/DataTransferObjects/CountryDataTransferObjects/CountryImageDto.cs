// CountryImageDto.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.DataTransferObjects.CountryDataTransferObjects;

public sealed class CountryImageDto
{
    public CountryImageDto(
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

    private CountryImageDto()
    {
        ImageBytes = Array.Empty<byte>();
        RelativeUrl = null!;
    }

    public int Id { get; private set; }

    public byte[] ImageBytes { get; private set; }

    public string RelativeUrl { get; private set; }

    public int CountryId { get; private set; }

    public static Expression<Func<CountryImage, CountryImageDto>> GetProjection()
    {
        return countryImage => new CountryImageDto
        {
            Id = countryImage.Id,
            ImageBytes = countryImage.ImageBytes,
            RelativeUrl = countryImage.RelativeUrl,
            CountryId = countryImage.CountryId,
        };
    }

    public CountryImage CreateCountryImage()
    {
        return new CountryImage(Id, ImageBytes, RelativeUrl, CountryId);
    }
}

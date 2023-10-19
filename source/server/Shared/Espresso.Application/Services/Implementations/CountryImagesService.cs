// CountryImagesService.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Services.Contracts;
using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Application.Services.Implementations;

public class CountryImagesService : ICountryImagesService
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;
    private readonly string _folderRootPath;

    public CountryImagesService(
        IEspressoDatabaseContext espressoDatabaseContext,
        string folderRootPath)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
        _folderRootPath = folderRootPath;
    }

    public async Task LoadImagesAndSaveToRootFolder()
    {
        var countryImages = await _espressoDatabaseContext
            .CountryImages
            .AsNoTracking()
            .ToArrayAsync();

        foreach (var countryImage in countryImages)
        {
            var fullImagePath = Path.Combine(_folderRootPath, countryImage.RelativeUrl);

            var imageDirectory = Path.GetDirectoryName(fullImagePath)!;

            if (!Directory.Exists(imageDirectory))
            {
                _ = Directory.CreateDirectory(imageDirectory);
            }

            using var imageFileStream = File.OpenWrite(fullImagePath);
            await imageFileStream.WriteAsync(countryImage.ImageBytes.AsMemory());
        }
    }
}

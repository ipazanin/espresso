// NewsPortalImagesService.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Services.Contracts;
using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Application.Services.Implementations;

public class NewsPortalImagesService : INewsPortalImagesService
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;
    private readonly string _folderRootPath;

    public NewsPortalImagesService(
        IEspressoDatabaseContext espressoDatabaseContext,
        string folderRootPath)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
        _folderRootPath = folderRootPath;
    }

    public async Task LoadImagesAndSaveToRootFolder()
    {
        var newsPortalImages = await _espressoDatabaseContext
            .NewsPortalImages
            .AsNoTracking()
            .Include(newsPortalImage => newsPortalImage.NewsPortal)
            .ToArrayAsync();

        foreach (var newsPortalImage in newsPortalImages)
        {
            var fullImagePath = Path.Combine(_folderRootPath, newsPortalImage.NewsPortal!.IconUrl);

            var imageDirectory = Path.GetDirectoryName(fullImagePath)!;

            if (!Directory.Exists(imageDirectory))
            {
                _ = Directory.CreateDirectory(imageDirectory);
            }

            using var imageFileStream = File.OpenWrite(fullImagePath);
            await imageFileStream.WriteAsync(newsPortalImage.ImageBytes.AsMemory());
        }
    }
}

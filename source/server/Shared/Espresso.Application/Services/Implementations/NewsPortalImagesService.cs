// NewsPortalImagesService.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Services.Contracts;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Application.Services.Implementations;

public class NewsPortalImagesService : INewsPortalImagesService
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _serverUrl;
    private readonly string _folderRootPath;

    public NewsPortalImagesService(
        IEspressoDatabaseContext espressoDatabaseContext,
        IHttpClientFactory httpClientFactory,
        string serverUrl,
        string folderRootPath)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
        _httpClientFactory = httpClientFactory;
        _serverUrl = serverUrl;
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

    public async Task DownloadImagesFromWebServer()
    {
        var newsPortalImages = await _espressoDatabaseContext
            .NewsPortalImages
            .AsNoTracking()
            .ToArrayAsync();

        var newsPortals = await _espressoDatabaseContext
            .NewsPortals
            .AsNoTracking()
            .ToArrayAsync();

        var newsPortalImagesToAdd = new List<NewsPortalImage>();
        var httpClient = _httpClientFactory.CreateClient();

        foreach (var newsPortal in newsPortals)
        {
            var newsPortalImage = Array.Find(newsPortalImages, newsPortalImage => newsPortalImage.NewsPortalId == newsPortal.Id);

            if (newsPortalImage is null)
            {
                try
                {
                    var downloadedImage = await DownloadImage(httpClient, newsPortal);

                    if (downloadedImage.Length == 0)
                    {
                        continue;
                    }

                    var newNewsPortalImage = new NewsPortalImage(
                        id: default,
                        imageBytes: downloadedImage,
                        newsPortalId: newsPortal.Id);
                    newsPortalImagesToAdd.Add(newNewsPortalImage);
                }
                catch
                {
                    // Image not found
                }
            }
        }

        _espressoDatabaseContext.NewsPortalImages.AddRange(newsPortalImagesToAdd);
        _ = await _espressoDatabaseContext.SaveChangesAsync(default);
    }

    private async Task<byte[]> DownloadImage(HttpClient httpClient, NewsPortal newsPortal)
    {
        var response = await httpClient.GetAsync($"{_serverUrl}/{newsPortal.IconUrl}");

        if (response is null)
        {
            return Array.Empty<byte>();
        }

        _ = response.EnsureSuccessStatusCode();

        var stringResponse = await response.Content.ReadAsStringAsync();

        if (stringResponse.StartsWith("<!doctype"))
        {
            return Array.Empty<byte>();
        }

        return await response.Content.ReadAsByteArrayAsync();
    }
}

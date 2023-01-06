// INewsPortalImagesService.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.Application.Services.Contracts;

public interface INewsPortalImagesService
{
    public Task LoadImagesAndSaveToRootFolder();

    public Task DownloadImagesFromWebServer();
}

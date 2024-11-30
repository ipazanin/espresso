// ImportNewsPortalImageCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Dashboard.Application.IServices;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.NewsPortalImage.Commands.ImportNewsPortalImage;

public class ImportNewsPortalImageCommandHandler : IRequestHandler<ImportNewsPortalImageCommand>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;
    private readonly IRefreshDashboardCacheService _refreshDashboardCacheService;
    private readonly ISendInformationToApiService _sendInformationToApiService;

    public ImportNewsPortalImageCommandHandler(
        IEspressoDatabaseContext espressoDatabaseContext,
        IRefreshDashboardCacheService refreshDashboardCacheService,
        ISendInformationToApiService sendInformationToApiService)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
        _refreshDashboardCacheService = refreshDashboardCacheService;
        _sendInformationToApiService = sendInformationToApiService;
    }

    public async Task Handle(ImportNewsPortalImageCommand request, CancellationToken cancellationToken)
    {
        if (request.NewsPortalImage is null)
        {
            return;
        }

        var newsPortalImageExistsInDatabase = await _espressoDatabaseContext
            .NewsPortalImages
            .AnyAsync(newsPortalImage => newsPortalImage.Id == request.NewsPortalImage.Id, cancellationToken);

        var newsPortalImage = request.NewsPortalImage.CreateNewsPortalImage();
        if (newsPortalImageExistsInDatabase)
        {
            _ = _espressoDatabaseContext.NewsPortalImages.Update(newsPortalImage);
        }
        else
        {
            _ = _espressoDatabaseContext.NewsPortalImages.Add(newsPortalImage);
        }

        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

        await _refreshDashboardCacheService.RefreshCache();
        await _sendInformationToApiService.SendCacheUpdatedNotification();
    }
}

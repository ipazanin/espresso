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

    public async Task<Unit> Handle(ImportNewsPortalImageCommand request, CancellationToken cancellationToken)
    {
        if (request.NewsPortalImage is null)
        {
            return Unit.Value;
        }

        var newsPortalImageExistsInDatabase = await _espressoDatabaseContext
            .NewsPortalImages
            .AnyAsync(newsPortalImage => newsPortalImage.Id == request.NewsPortalImage.Id, cancellationToken);

        var newsPortalImage = request.NewsPortalImage.CreateNewsPortalImage();
        if (newsPortalImageExistsInDatabase)
        {
            _espressoDatabaseContext.NewsPortalImages.Update(newsPortalImage);
        }
        else
        {
            _espressoDatabaseContext.NewsPortalImages.Add(newsPortalImage);
        }

        await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

        await _refreshDashboardCacheService.RefreshCache();
        await _sendInformationToApiService.SendCacheUpdatedNotification();

        return Unit.Value;
    }
}

// CreateNewsPortalCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using Espresso.Dashboard.Application.IServices;
using Espresso.Persistence.Database;
using MediatR;

namespace Espresso.Dashboard.Application.NewsPortals.Commands.CreateNewsPortal;

public class CreateNewsPortalCommandHandler : IRequestHandler<CreateNewsPortalCommand>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;
    private readonly IRefreshDashboardCacheService _refreshDashboardCacheService;
    private readonly ISendInformationToApiService _sendInformationToApiService;

    public CreateNewsPortalCommandHandler(
        IEspressoDatabaseContext espressoDatabaseContext,
        IRefreshDashboardCacheService refreshDashboardCacheService,
        ISendInformationToApiService sendInformationToApiService)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
        _refreshDashboardCacheService = refreshDashboardCacheService;
        _sendInformationToApiService = sendInformationToApiService;
    }

    public async Task Handle(CreateNewsPortalCommand request, CancellationToken cancellationToken)
    {
        var updatedNewsPortal = request.NewsPortal.CreateNewsPortal();

        _ = _espressoDatabaseContext.NewsPortals.Add(updatedNewsPortal);

        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

        if (request.NewsPortalImage is not null)
        {
            var newsPortalImageDto = new NewsPortalImageDto(
                id: default,
                imageBytes: request.NewsPortalImage.ImageBytes,
                newsPortalId: updatedNewsPortal.Id);
            var newsPortalImage = newsPortalImageDto.CreateNewsPortalImage();
            _ = _espressoDatabaseContext.NewsPortalImages.Add(newsPortalImage);
            _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);
        }

        await _refreshDashboardCacheService.RefreshCache();
        await _sendInformationToApiService.SendCacheUpdatedNotification();
    }
}

// DeleteNewsPortalCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Dashboard.Application.IServices;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.NewsPortals.Commands.DeleteNewsPortal;

public class DeleteNewsPortalCommandHandler : IRequestHandler<DeleteNewsPortalCommand>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;
    private readonly IRefreshDashboardCacheService _refreshDashboardCacheService;
    private readonly ISendInformationToApiService _sendInformationToApiService;

    public DeleteNewsPortalCommandHandler(
        IEspressoDatabaseContext espressoDatabaseContext,
        IRefreshDashboardCacheService refreshDashboardCacheService,
        ISendInformationToApiService sendInformationToApiService)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
        _refreshDashboardCacheService = refreshDashboardCacheService;
        _sendInformationToApiService = sendInformationToApiService;
    }

    public async Task Handle(DeleteNewsPortalCommand request, CancellationToken cancellationToken)
    {
        var newsPortalToDelete = await _espressoDatabaseContext
            .NewsPortals
            .FirstAsync(newsPortal => newsPortal.Id == request.NewsPortalId, cancellationToken);

        _ = _espressoDatabaseContext.NewsPortals.Remove(newsPortalToDelete);

        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

        await _refreshDashboardCacheService.RefreshCache();
        await _sendInformationToApiService.SendCacheUpdatedNotification();
    }
}

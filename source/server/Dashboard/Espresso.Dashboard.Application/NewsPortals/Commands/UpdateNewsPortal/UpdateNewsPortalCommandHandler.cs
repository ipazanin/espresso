// UpdateNewsPortalCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Dashboard.Application.IServices;
using Espresso.Persistence.Database;
using MediatR;

namespace Espresso.Dashboard.Application.NewsPortals.Commands.UpdateNewsPortal;

public class UpdateNewsPortalCommandHandler : IRequestHandler<UpdateNewsPortalCommand>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;
    private readonly IRefreshDashboardCacheService _refreshDashboardCacheService;
    private readonly ISendInformationToApiService _sendInformationToApiService;

    public UpdateNewsPortalCommandHandler(
        IEspressoDatabaseContext espressoDatabaseContext,
        IRefreshDashboardCacheService refreshDashboardCacheService,
        ISendInformationToApiService sendInformationToApiService)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
        _refreshDashboardCacheService = refreshDashboardCacheService;
        _sendInformationToApiService = sendInformationToApiService;
    }

    public async Task<Unit> Handle(UpdateNewsPortalCommand request, CancellationToken cancellationToken)
    {
        var updatedNewsPortal = request.NewsPortal.CreateNewsPortal();

        _ = _espressoDatabaseContext.NewsPortals.Update(updatedNewsPortal);

        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

        await _refreshDashboardCacheService.RefreshCache();
        await _sendInformationToApiService.SendCacheUpdatedNotification();

        return Unit.Value;
    }
}

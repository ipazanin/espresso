// DeleteRegionCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Dashboard.Application.IServices;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.Regions.Commands.DeleteRegion;

public class DeleteRegionCommandHandler : IRequestHandler<DeleteRegionCommand>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;
    private readonly IRefreshDashboardCacheService _refreshDashboardCacheService;
    private readonly ISendInformationToApiService _sendInformationToApiService;

    public DeleteRegionCommandHandler(
        IEspressoDatabaseContext espressoDatabaseContext,
        IRefreshDashboardCacheService refreshDashboardCacheService,
        ISendInformationToApiService sendInformationToApiService)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
        _refreshDashboardCacheService = refreshDashboardCacheService;
        _sendInformationToApiService = sendInformationToApiService;
    }

    public async Task<Unit> Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
    {
        var regionToDelete = await _espressoDatabaseContext
            .Regions
            .FirstAsync(region => region.Id == request.RegionId, cancellationToken);

        _ = _espressoDatabaseContext.Regions.Remove(regionToDelete);

        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

        await _refreshDashboardCacheService.RefreshCache();
        await _sendInformationToApiService.SendCacheUpdatedNotification();

        return Unit.Value;
    }
}

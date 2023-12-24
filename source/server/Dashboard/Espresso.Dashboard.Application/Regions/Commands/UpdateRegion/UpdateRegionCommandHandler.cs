// UpdateRegionCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Dashboard.Application.IServices;
using Espresso.Persistence.Database;
using MediatR;

namespace Espresso.Dashboard.Application.Regions.Commands.UpdateRegion;

public class UpdateRegionCommandHandler : IRequestHandler<UpdateRegionCommand>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;
    private readonly ISendInformationToApiService _sendInformationToApiService;
    private readonly IRefreshDashboardCacheService _refreshDashboardCacheService;

    public UpdateRegionCommandHandler(
        IEspressoDatabaseContext espressoDatabaseContext,
        ISendInformationToApiService sendInformationToApiService,
        IRefreshDashboardCacheService refreshDashboardCacheService)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
        _sendInformationToApiService = sendInformationToApiService;
        _refreshDashboardCacheService = refreshDashboardCacheService;
    }

    public async Task Handle(UpdateRegionCommand request, CancellationToken cancellationToken)
    {
        var updatedRegion = request.Region.CreateRegion();

        _ = _espressoDatabaseContext.Regions.Update(updatedRegion);

        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

        await _refreshDashboardCacheService.RefreshCache();
        await _sendInformationToApiService.SendCacheUpdatedNotification();
    }
}

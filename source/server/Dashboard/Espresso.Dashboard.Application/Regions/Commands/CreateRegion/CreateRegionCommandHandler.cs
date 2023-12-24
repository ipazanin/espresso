// CreateRegionCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Dashboard.Application.IServices;
using Espresso.Persistence.Database;
using MediatR;

namespace Espresso.Dashboard.Application.Regions.Commands.CreateRegion;

public class CreateRegionCommandHandler : IRequestHandler<CreateRegionCommand>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;
    private readonly IRefreshDashboardCacheService _refreshDashboardCacheService;
    private readonly ISendInformationToApiService _sendInformationToApiService;

    public CreateRegionCommandHandler(
        IEspressoDatabaseContext espressoDatabaseContext,
        IRefreshDashboardCacheService refreshDashboardCacheService,
        ISendInformationToApiService sendInformationToApiService)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
        _refreshDashboardCacheService = refreshDashboardCacheService;
        _sendInformationToApiService = sendInformationToApiService;
    }

    public async Task Handle(CreateRegionCommand request, CancellationToken cancellationToken)
    {
        var createdRegion = request.Region.CreateRegion();

        _ = _espressoDatabaseContext.Regions.Add(createdRegion);

        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

        await _refreshDashboardCacheService.RefreshCache();
        await _sendInformationToApiService.SendCacheUpdatedNotification();
    }
}

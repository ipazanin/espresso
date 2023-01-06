// UpdateCategoryCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Dashboard.Application.IServices;
using Espresso.Persistence.Database;
using MediatR;

namespace Espresso.Dashboard.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;
    private readonly ISendInformationToApiService _sendInformationToApiService;
    private readonly IRefreshDashboardCacheService _refreshDashboardCacheService;

    public UpdateCategoryCommandHandler(
        IEspressoDatabaseContext espressoDatabaseContext,
        ISendInformationToApiService sendInformationToApiService,
        IRefreshDashboardCacheService refreshDashboardCacheService)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
        _sendInformationToApiService = sendInformationToApiService;
        _refreshDashboardCacheService = refreshDashboardCacheService;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var updatedCategory = request.Category.CreateCategory();

        _ = _espressoDatabaseContext.Categories.Update(updatedCategory);
        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

        await _sendInformationToApiService.SendCacheUpdatedNotification();
        await _refreshDashboardCacheService.RefreshCache();

        return Unit.Value;
    }
}

// DeleteCategoryCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Dashboard.Application.Categories.Commands.UpdateCategory;
using Espresso.Dashboard.Application.IServices;
using Espresso.Persistence.Database;
using MediatR;

namespace Espresso.Dashboard.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;
    private readonly ISendInformationToApiService _sendInformationToApiService;
    private readonly IRefreshDashboardCacheService _refreshDashboardCacheService;

    public DeleteCategoryCommandHandler(
        IEspressoDatabaseContext espressoDatabaseContext,
        ISendInformationToApiService sendInformationToApiService,
        IRefreshDashboardCacheService refreshDashboardCacheService)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
        _sendInformationToApiService = sendInformationToApiService;
        _refreshDashboardCacheService = refreshDashboardCacheService;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryToRemove = await _espressoDatabaseContext
            .Categories
            .FindAsync(keyValues: new object[] { request.CategoryId }, cancellationToken: cancellationToken);

        if (categoryToRemove is null)
        {
            return Unit.Value;
        }

        _ = _espressoDatabaseContext.Categories.Remove(categoryToRemove);
        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

        await _sendInformationToApiService.SendCacheUpdatedNotification();
        await _refreshDashboardCacheService.RefreshCache();

        return Unit.Value;
    }
}

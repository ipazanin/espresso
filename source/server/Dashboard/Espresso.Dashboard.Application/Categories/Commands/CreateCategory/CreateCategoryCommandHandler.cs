// CreateCategoryCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Dashboard.Application.Categories.Commands.UpdateCategory;
using Espresso.Dashboard.Application.IServices;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using MediatR;

namespace Espresso.Dashboard.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;
    private readonly ISendInformationToApiService _sendInformationToApiService;
    private readonly IRefreshDashboardCacheService _refreshDashboardCacheService;

    public CreateCategoryCommandHandler(
        IEspressoDatabaseContext espressoDatabaseContext,
        ISendInformationToApiService sendInformationToApiService,
        IRefreshDashboardCacheService refreshDashboardCacheService)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
        _sendInformationToApiService = sendInformationToApiService;
        _refreshDashboardCacheService = refreshDashboardCacheService;
    }

    public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var updatedCategory = new Category(
            id: request.Category.Id,
            name: request.Category.Name,
            color: request.Category.Color,
            keyWordsRegexPattern: request.Category.KeyWordsRegexPattern,
            sortIndex: request.Category.SortIndex,
            position: request.Category.Position,
            categoryType: request.Category.CategoryType,
            categoryUrl: string.Empty);

        _ = _espressoDatabaseContext.Categories.Add(updatedCategory);
        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

        await _sendInformationToApiService.SendCacheUpdatedNotification();
        await _refreshDashboardCacheService.RefreshCache();

        return Unit.Value;
    }
}

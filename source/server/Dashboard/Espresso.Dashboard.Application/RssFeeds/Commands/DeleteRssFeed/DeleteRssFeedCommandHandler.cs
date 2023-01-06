// DeleteRssFeedCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Dashboard.Application.IServices;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.RssFeeds.Commands.DeleteRssFeed;

public class DeleteRssFeedCommandHandler : IRequestHandler<DeleteRssFeedCommand>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;
    private readonly IRefreshDashboardCacheService _refreshDashboardCacheService;
    private readonly ISendInformationToApiService _sendInformationToApiService;

    public DeleteRssFeedCommandHandler(
        IEspressoDatabaseContext espressoDatabaseContext,
        IRefreshDashboardCacheService refreshDashboardCacheService,
        ISendInformationToApiService sendInformationToApiService)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
        _refreshDashboardCacheService = refreshDashboardCacheService;
        _sendInformationToApiService = sendInformationToApiService;
    }

    public async Task<Unit> Handle(DeleteRssFeedCommand request, CancellationToken cancellationToken)
    {
        var rssFeedToDelete = await _espressoDatabaseContext
            .RssFeeds
            .FirstAsync(rssFeed => rssFeed.Id == request.RssFeedId, cancellationToken);

        _ = _espressoDatabaseContext
            .RssFeeds
            .Remove(rssFeedToDelete);

        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

        await _refreshDashboardCacheService.RefreshCache();
        await _sendInformationToApiService.SendCacheUpdatedNotification();

        return Unit.Value;
    }
}

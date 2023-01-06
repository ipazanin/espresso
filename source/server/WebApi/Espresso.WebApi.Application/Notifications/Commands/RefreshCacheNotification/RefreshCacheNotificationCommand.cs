// RefreshCacheNotificationCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.WebApi.Application.Initialization;
using MediatR;

namespace Espresso.WebApi.Application.Notifications.Commands.RefreshCacheNotification;

public class RefreshCacheNotificationCommandHandler : IRequestHandler<RefreshCacheNotificationCommand>
{
    private readonly IRefreshWebApiCache _refreshWebApiCache;

    public RefreshCacheNotificationCommandHandler(IRefreshWebApiCache refreshWebApiCache)
    {
        _refreshWebApiCache = refreshWebApiCache;
    }

    public async Task<Unit> Handle(RefreshCacheNotificationCommand request, CancellationToken cancellationToken)
    {
        await _refreshWebApiCache.RefreshCacheValues();

        return Unit.Value;
    }
}

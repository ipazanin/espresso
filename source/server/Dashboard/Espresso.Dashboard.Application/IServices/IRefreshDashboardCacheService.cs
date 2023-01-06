// IRefreshDashboardCacheService.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.Dashboard.Application.IServices;

public interface IRefreshDashboardCacheService
{
    public Task RefreshCache();
}

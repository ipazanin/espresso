// IRefreshWebApiCache.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Initialization;

public interface IRefreshWebApiCache
{
    public Task RefreshCacheValues();
}

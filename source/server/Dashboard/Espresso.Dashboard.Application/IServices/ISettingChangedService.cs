// ISettingChangedService.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;

namespace Espresso.Dashboard.Application.IServices;

public interface ISettingChangedService
{
    public Task UpdateSetting(Setting setting, CancellationToken cancellationToken);
}

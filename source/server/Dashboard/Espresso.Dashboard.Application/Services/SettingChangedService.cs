// SettingChangedService.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Dashboard.Application.IServices;
using Espresso.Domain.Entities;
using Espresso.Domain.Infrastructure;
using Espresso.Persistence.Database;

namespace Espresso.Dashboard.Application.Services;

public class SettingChangedService : ISettingChangedService
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;
    private readonly ISettingProvider _settingProvider;
    private readonly ISendInformationToApiService _sendInformationToApiService;

    public SettingChangedService(
        IEspressoDatabaseContext espressoDatabaseContext,
        ISettingProvider settingProvider,
        ISendInformationToApiService sendInformationToApiService)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
        _settingProvider = settingProvider;
        _sendInformationToApiService = sendInformationToApiService;
    }

    public async Task UpdateSetting(Setting setting, CancellationToken cancellationToken)
    {
        _ = _espressoDatabaseContext.Settings.Add(setting);
        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

        await _settingProvider.UpdateLatestSetting(cancellationToken);

        await _sendInformationToApiService.SendSettingUpdatedNotification(cancellationToken);
    }
}

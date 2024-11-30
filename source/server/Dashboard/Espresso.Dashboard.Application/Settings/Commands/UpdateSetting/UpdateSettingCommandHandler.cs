// UpdateSettingCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Dashboard.Application.IServices;
using Espresso.Domain.Infrastructure;
using Espresso.Persistence.Database;
using MediatR;

namespace Espresso.Dashboard.Application.Settings.Commands.UpdateSetting;

public class UpdateSettingCommandHandler : IRequestHandler<UpdateSettingCommand>
{
    private readonly IEspressoDatabaseContext _espressoDatabaseContext;
    private readonly ISettingProvider _settingProvider;
    private readonly ISendInformationToApiService _sendInformationToApiService;

    public UpdateSettingCommandHandler(
        IEspressoDatabaseContext espressoDatabaseContext,
        ISettingProvider settingProvider,
        ISendInformationToApiService sendInformationToApiService)
    {
        _espressoDatabaseContext = espressoDatabaseContext;
        _settingProvider = settingProvider;
        _sendInformationToApiService = sendInformationToApiService;
    }

    public async Task Handle(UpdateSettingCommand request, CancellationToken cancellationToken)
    {
        var updatedSetting = request.Setting.CreateSetting();

        _ = _espressoDatabaseContext.Settings.Update(updatedSetting);

        _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

        await _settingProvider.UpdateLatestSetting(default);
        await _sendInformationToApiService.SendSettingUpdatedNotification();
    }
}

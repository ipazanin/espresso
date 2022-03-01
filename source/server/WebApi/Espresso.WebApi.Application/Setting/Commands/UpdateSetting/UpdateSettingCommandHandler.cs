// UpdateSettingCommandHandler.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Infrastructure;
using MediatR;

namespace Espresso.WebApi.Application.Setting.Commands.UpdateSetting;

public class UpdateSettingCommandHandler : IRequestHandler<UpdateSettingCommand, UpdateSettingCommandResponse>
{
    private readonly ISettingProvider _settingProvider;

    public UpdateSettingCommandHandler(ISettingProvider settingProvider)
    {
        _settingProvider = settingProvider;
    }

    public async Task<UpdateSettingCommandResponse> Handle(UpdateSettingCommand request, CancellationToken cancellationToken)
    {
        await _settingProvider.UpdateLatestSetting(cancellationToken);

        return new();
    }
}

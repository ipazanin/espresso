// UpdateSettingCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Infrastructure;
using MediatR;

namespace Espresso.WebApi.Application.Setting.Commands.UpdateSetting;

public class UpdateSettingCommandHandler : IRequestHandler<UpdateSettingCommand>
{
    private readonly ISettingProvider _settingProvider;

    public UpdateSettingCommandHandler(ISettingProvider settingProvider)
    {
        _settingProvider = settingProvider;
    }

#pragma warning disable AsyncFixer01 // Unnecessary async/await usage
    public async Task Handle(UpdateSettingCommand request, CancellationToken cancellationToken)
    {
        await _settingProvider.UpdateLatestSetting(cancellationToken);
    }
#pragma warning restore AsyncFixer01 // Unnecessary async/await usage
}

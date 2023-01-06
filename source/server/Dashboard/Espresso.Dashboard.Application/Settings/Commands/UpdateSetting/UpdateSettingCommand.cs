// UpdateSettingCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.SettingDataTransferObjects;
using MediatR;

namespace Espresso.Dashboard.Application.Settings.Commands.UpdateSetting;

public class UpdateSettingCommand : IRequest
{
    public UpdateSettingCommand(SettingDto setting)
    {
        Setting = setting;
    }

    public SettingDto Setting { get; }
}

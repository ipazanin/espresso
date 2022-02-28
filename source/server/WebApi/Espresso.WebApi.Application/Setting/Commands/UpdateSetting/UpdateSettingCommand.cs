// UpdateSettingCommand.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;
using MediatR;

namespace Espresso.WebApi.Application.Setting.Commands.UpdateSetting;

public record UpdateSettingCommand : Request<UpdateSettingCommandResponse>
{
}

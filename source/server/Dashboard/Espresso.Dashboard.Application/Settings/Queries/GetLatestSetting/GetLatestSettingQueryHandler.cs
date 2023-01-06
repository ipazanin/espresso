// GetLatestSettingQueryHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.SettingDataTransferObjects;
using Espresso.Domain.Infrastructure;
using MediatR;

namespace Espresso.Dashboard.Application.Settings.Queries.GetLatestSetting;

public class GetLatestSettingQueryHandler : IRequestHandler<GetLatestSettingQuery, GetLatestSettingQueryResponse>
{
    private readonly ISettingProvider _settingProvider;

    public GetLatestSettingQueryHandler(ISettingProvider settingProvider)
    {
        _settingProvider = settingProvider;
    }

    public Task<GetLatestSettingQueryResponse> Handle(GetLatestSettingQuery request, CancellationToken cancellationToken)
    {
        var latestSetting = _settingProvider.LatestSetting;

        var settingDto = SettingDto.Projection.Compile().Invoke(latestSetting);

        var response = new GetLatestSettingQueryResponse(settingDto);

        return Task.FromResult(response);
    }
}

// GetLatestSettingQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.SettingDataTransferObjects;

namespace Espresso.Dashboard.Application.Settings.Queries.GetLatestSetting;

public class GetLatestSettingQueryResponse
{
    public GetLatestSettingQueryResponse(SettingDto setting)
    {
        Setting = setting;
    }

    public SettingDto Setting { get; }
}

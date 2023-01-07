// NewsPortalSettingDto.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.ValueObjects.SettingsValueObjects;

namespace Espresso.Application.DataTransferObjects.SettingDataTransferObjects;

public sealed class NewsPortalSettingDto
{
    public NewsPortalSettingDto(double maxAgeOfNewNewsPortalInDays, int newNewsPortalsPositionInApp)
    {
        MaxAgeOfNewNewsPortalInDays = maxAgeOfNewNewsPortalInDays;
        NewNewsPortalsPositionInApp = newNewsPortalsPositionInApp;
    }

    private NewsPortalSettingDto()
    {
    }

    public static Expression<Func<NewsPortalSetting, NewsPortalSettingDto>> Projection
    {
        get => newsPortalSetting => new NewsPortalSettingDto
        {
            MaxAgeOfNewNewsPortalInDays = newsPortalSetting.MaxAgeOfNewNewsPortal.TotalDays,
            NewNewsPortalsPositionInApp = newsPortalSetting.NewNewsPortalsPosition,
        };
    }

    public double MaxAgeOfNewNewsPortalInDays { get; set; }

    public int NewNewsPortalsPositionInApp { get; set; }

    public NewsPortalSetting CreateNewsPortalSetting()
    {
        return new NewsPortalSetting(
            maxAgeOfNewNewsPortalInMiliseconds: (long)TimeSpan.FromDays(MaxAgeOfNewNewsPortalInDays).TotalMilliseconds,
            newNewsPortalsPosition: NewNewsPortalsPositionInApp);
    }
}

// ApplicationStatistics.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.Application.Models;

/// <summary>
///  Application statistics data.
/// </summary>
public class ApplicationStatistics
{
    public ApplicationStatistics(
            int yesterdayAndroidCount,
            int yesterdayIosCount,
            int totalAndroidCount,
            int totalIosCount,
            int activeUsersOnAndroid,
            int activeUsersOnIos,
            decimal androidRevenueToday,
            decimal iosRevenueToday,
            decimal revenueCurrentMonth,
            decimal revenuePreviousMonth)
    {
        YesterdayAndroidCount = yesterdayAndroidCount;
        YesterdayIosCount = yesterdayIosCount;
        TotalAndroidCount = totalAndroidCount;
        TotalIosCount = totalIosCount;
        ActiveUsersOnAndroid = activeUsersOnAndroid;
        ActiveUsersOnIos = activeUsersOnIos;
        AndroidRevenueToday = androidRevenueToday;
        IosRevenueToday = iosRevenueToday;
        RevenueCurrentMonth = revenueCurrentMonth;
        RevenuePreviousMonth = revenuePreviousMonth;
    }

    public int YesterdayAndroidCount { get; }

    public int YesterdayIosCount { get; }

    public int TotalAndroidCount { get; }

    public int TotalIosCount { get; }

    public int ActiveUsersOnAndroid { get; }

    public int ActiveUsersOnIos { get; }

    public decimal AndroidRevenueToday { get; }

    public decimal IosRevenueToday { get; }

    public decimal RevenueCurrentMonth { get; }

    public decimal RevenuePreviousMonth { get; }
}

// HomeBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Dashboard.Configuration;
using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.Home;

public class HomeBase : ComponentBase
{
    protected string DashboardVersion => DashboardConfiguration.AppConfiguration.Version;

    [Inject]
    private IDashboardConfiguration DashboardConfiguration { get; init; } = null!;
}

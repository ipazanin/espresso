// MainLayoutBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MudBlazor;

namespace Espresso.Dashboard.Shared;

public class MainLayoutBase : LayoutComponentBase
{
    protected bool DrawerOpen { get; set; } = true;

    protected MudTheme Theme { get; } = new();

    protected bool IsDarkMode { get; set; }

    [CascadingParameter]
    private Task<AuthenticationState> AuthState { get; init; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; init; } = null!;

    [Inject]
    private ProtectedLocalStorage ProtectedLocalStorage { get; init; } = null!;

    protected override async Task OnInitializedAsync()
    {
        OnInitialized();

        var user = (await AuthState).User;
        if (user.Identity?.IsAuthenticated == false)
        {
            NavigationManager.NavigateTo("Identity/Account/Login");
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            return;
        }

        var isDarkModeStorageResult = await ProtectedLocalStorage.GetAsync<bool>(nameof(IsDarkMode));
        if (isDarkModeStorageResult.Success)
        {
            IsDarkMode = isDarkModeStorageResult.Value;
            StateHasChanged();
        }
    }

    protected void DrawerToggle()
    {
        DrawerOpen = !DrawerOpen;
        StateHasChanged();
    }

    protected async Task ThemeButtonClicked()
    {
        IsDarkMode = !IsDarkMode;
        await ProtectedLocalStorage.SetAsync(nameof(IsDarkMode), IsDarkMode);
        StateHasChanged();
    }
}

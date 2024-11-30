// MainLayoutBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MudBlazor;

namespace Espresso.Dashboard.SharedComponents;

public class MainLayoutBase : LayoutComponentBase
{
    protected bool DrawerOpen { get; set; } = true;

    protected MudTheme Theme { get; } = new();

#pragma warning disable S125 // Sections of code should not be commented out
#pragma warning disable SA1515 // Single-line comment should be preceded by blank line
#pragma warning disable SA1005 // Single line comments should begin with single space
    //{
    //    Palette = new()
    //    {
    //        Primary = "#d7545a",
    //        Secondary = "#1d1d1b66",
    //        AppbarBackground = "#d7545a",
    //    },
    //    PaletteDark = new(),
    //    LayoutProperties = new(),
    //};
#pragma warning restore S125 // Sections of code should not be commented out
#pragma warning restore SA1005 // Single line comments should begin with single space
#pragma warning restore SA1515 // Single-line comment should be preceded by blank line

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

#pragma warning disable VSTHRD003 // Avoid awaiting foreign Tasks
        var user = (await AuthState).User;
#pragma warning restore VSTHRD003 // Avoid awaiting foreign Tasks
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

        try
        {
            var isDarkModeStorageResult = await ProtectedLocalStorage.GetAsync<bool>(nameof(IsDarkMode));
            if (isDarkModeStorageResult.Success)
            {
                IsDarkMode = isDarkModeStorageResult.Value;
                StateHasChanged();
            }
        }
        catch
        {
            // if session got corrupted
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

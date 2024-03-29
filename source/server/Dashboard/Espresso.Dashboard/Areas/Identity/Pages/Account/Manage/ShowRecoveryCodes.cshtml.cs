﻿// ShowRecoveryCodes.cshtml.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Espresso.Dashboard.Areas.Identity.Pages.Account.Manage;
#pragma warning disable SA1649 // File name should match first type name
public class ShowRecoveryCodesModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
{
#pragma warning disable CA1819 // Properties should not return arrays
    [TempData]
    public string[]? RecoveryCodes { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays

    [TempData]
    public string? StatusMessage { get; set; }

    public IActionResult OnGet()
    {
        if (RecoveryCodes == null || RecoveryCodes.Length == 0)
        {
            return RedirectToPage("./TwoFactorAuthentication");
        }

        return Page();
    }
}

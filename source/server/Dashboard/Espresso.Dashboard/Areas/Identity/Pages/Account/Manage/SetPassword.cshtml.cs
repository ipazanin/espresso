﻿// SetPassword.cshtml.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Espresso.Dashboard.Areas.Identity.Pages.Account.Manage;
#pragma warning disable SA1649 // File name should match first type name
public class SetPasswordModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="SetPasswordModel"/> class.
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="signInManager"></param>
    public SetPasswordModel(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [BindProperty]
    public SetPasswordInputModel Input { get; set; } = null!;

    [TempData]
    public string? StatusMessage { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
#pragma warning disable SA1201 // Elements should appear in the correct order
    public async Task<IActionResult> OnGetAsync()
#pragma warning restore SA1201 // Elements should appear in the correct order
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        var hasPassword = await _userManager.HasPasswordAsync(user);

        if (hasPassword)
        {
            return RedirectToPage("./ChangePassword");
        }

        return Page();
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        var addPasswordResult = await _userManager.AddPasswordAsync(user, Input.NewPassword);
        if (!addPasswordResult.Succeeded)
        {
            foreach (var error in addPasswordResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }

        await _signInManager.RefreshSignInAsync(user);
        StatusMessage = "Your password has been set.";

        return RedirectToPage();
    }
}

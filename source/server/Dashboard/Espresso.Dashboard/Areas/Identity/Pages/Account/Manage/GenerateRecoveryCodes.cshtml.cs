// GenerateRecoveryCodes.cshtml.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Espresso.Dashboard.Areas.Identity.Pages.Account.Manage;
#pragma warning disable SA1649 // File name should match first type name
public class GenerateRecoveryCodesModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<GenerateRecoveryCodesModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenerateRecoveryCodesModel"/> class.
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="logger"></param>
    public GenerateRecoveryCodesModel(
        UserManager<IdentityUser> userManager,
        ILogger<GenerateRecoveryCodesModel> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    [TempData]
#pragma warning disable CA1819 // Properties should not return arrays
    public string[]? RecoveryCodes { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays

    [TempData]
    public string? StatusMessage { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        var isTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
        if (!isTwoFactorEnabled)
        {
            var userId = await _userManager.GetUserIdAsync(user);
            throw new InvalidOperationException($"Cannot generate recovery codes for user with ID '{userId}' because they do not have 2FA enabled.");
        }

        return Page();
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        var isTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
        var userId = await _userManager.GetUserIdAsync(user);
        if (!isTwoFactorEnabled)
        {
            throw new InvalidOperationException($"Cannot generate recovery codes for user with ID '{userId}' as they do not have 2FA enabled.");
        }

        var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
        RecoveryCodes = recoveryCodes?.ToArray() ?? [];

        _logger.LogInformation("User with ID '{UserId}' has generated new 2FA recovery codes.", userId);
        StatusMessage = "You have generated new recovery codes.";
        return RedirectToPage("./ShowRecoveryCodes");
    }
}

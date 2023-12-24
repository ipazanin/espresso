// EnableAuthenticator.cshtml.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Globalization;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Espresso.Dashboard.Areas.Identity.Pages.Account.Manage;

#pragma warning disable SA1649 // File name should match first type name
public class EnableAuthenticatorModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
{
    private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

    private static readonly CompositeFormat s_authenticatorUriCompositeFormat = CompositeFormat.Parse(AuthenticatorUriFormat);

    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<EnableAuthenticatorModel> _logger;
    private readonly UrlEncoder _urlEncoder;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnableAuthenticatorModel"/> class.
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="logger"></param>
    /// <param name="urlEncoder"></param>
    public EnableAuthenticatorModel(
        UserManager<IdentityUser> userManager,
        ILogger<EnableAuthenticatorModel> logger,
        UrlEncoder urlEncoder)
    {
        _userManager = userManager;
        _logger = logger;
        _urlEncoder = urlEncoder;
    }

    public string SharedKey { get; set; } = null!;

    public string AuthenticatorUri { get; set; } = null!;

#pragma warning disable CA1819 // Properties should not return arrays
    [TempData]
    public string[]? RecoveryCodes { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays

    [TempData]
    public string? StatusMessage { get; set; }

    [BindProperty]
    public EnableAuthenticatorInputModel Input { get; set; } = null!;

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

        await LoadSharedKeyAndQrCodeUriAsync(user);

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

        if (!ModelState.IsValid)
        {
            await LoadSharedKeyAndQrCodeUriAsync(user);
            return Page();
        }

        // Strip spaces and hyphens
        var verificationCode = Input
            .Code
            .Replace(" ", string.Empty, StringComparison.Ordinal)
            .Replace("-", string.Empty, StringComparison.Ordinal);

        var is2faTokenValid = await _userManager.VerifyTwoFactorTokenAsync(
            user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);

        if (!is2faTokenValid)
        {
            ModelState.AddModelError("Input.Code", "Verification code is invalid.");
            await LoadSharedKeyAndQrCodeUriAsync(user);
            return Page();
        }

        _ = await _userManager.SetTwoFactorEnabledAsync(user, true);
        var userId = await _userManager.GetUserIdAsync(user);
        _logger.LogInformation("User with ID '{UserId}' has enabled 2FA with an authenticator app.", userId);

        StatusMessage = "Your authenticator app has been verified.";

        if (await _userManager.CountRecoveryCodesAsync(user) == 0)
        {
            var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
            RecoveryCodes = recoveryCodes?.ToArray() ?? Array.Empty<string>();
            return RedirectToPage("./ShowRecoveryCodes");
        }

        return RedirectToPage("./TwoFactorAuthentication");
    }

    private static string FormatKey(string unformattedKey)
    {
        var result = new StringBuilder();
        var currentPosition = 0;
        while (currentPosition + 4 < unformattedKey.Length)
        {
            _ = result.Append(unformattedKey.AsSpan(currentPosition, 4)).Append(' ');
            currentPosition += 4;
        }

        if (currentPosition < unformattedKey.Length)
        {
            _ = result.Append(unformattedKey[currentPosition..]);
        }

#pragma warning disable CA1308 // Normalize strings to uppercase
        return result.ToString().ToLowerInvariant();
#pragma warning restore CA1308 // Normalize strings to uppercase
    }

    private async Task LoadSharedKeyAndQrCodeUriAsync(IdentityUser user)
    {
        // Load the authenticator key & QR code URI to display on the form
        var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
        if (string.IsNullOrEmpty(unformattedKey))
        {
            _ = await _userManager.ResetAuthenticatorKeyAsync(user);
            unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
        }

        SharedKey = FormatKey(unformattedKey!);

        var email = await _userManager.GetEmailAsync(user)!;
        AuthenticatorUri = GenerateQrCodeUri(email!, unformattedKey!);
    }

    private string GenerateQrCodeUri(string email, string unformattedKey)
    {
        return string.Format(
            CultureInfo.InvariantCulture,
            format: s_authenticatorUriCompositeFormat,
            arg0: _urlEncoder.Encode("Espresso.Dashboard"),
            arg1: _urlEncoder.Encode(email),
            arg2: unformattedKey);
    }
}

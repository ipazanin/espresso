// LoginWith2fa.cshtml.cs
//
// © 2022 Espresso News. All rights reserved.

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Espresso.Dashboard.Areas.Identity.Pages.Account;

[AllowAnonymous]
#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable SA1649 // File name should match first type name
public class LoginWith2faModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
#pragma warning restore S101 // Types should be named in PascalCase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<LoginWith2faModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginWith2faModel"/> class.
    /// </summary>
    /// <param name="signInManager"></param>
    /// <param name="logger"></param>
    public LoginWith2faModel(SignInManager<IdentityUser> signInManager, ILogger<LoginWith2faModel> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }

    [BindProperty]
    public InputModel Input { get; set; } = null!;

    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }

    public class InputModel
    {
        [Required]
        [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Authenticator code")]
        public string TwoFactorCode { get; set; } = null!;

        [Display(Name = "Remember this machine")]
        public bool RememberMachine { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="rememberMe"></param>
    /// <param name="returnUrl"></param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
#pragma warning disable SA1201 // Elements should appear in the correct order
    public async Task<IActionResult> OnGetAsync(bool rememberMe, string? returnUrl = null)
#pragma warning restore SA1201 // Elements should appear in the correct order
    {
        // Ensure the user has gone through the username & password screen first
        var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

        if (user == null)
        {
            throw new InvalidOperationException("Unable to load two-factor authentication user.");
        }

        ReturnUrl = returnUrl;
        RememberMe = rememberMe;

        return Page();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="rememberMe"></param>
    /// <param name="returnUrl"></param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public async Task<IActionResult> OnPostAsync(bool rememberMe, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        returnUrl ??= Url.Content("~/");

        var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        if (user == null)
        {
            throw new InvalidOperationException("Unable to load two-factor authentication user.");
        }

        var authenticatorCode = Input.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

        var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, Input.RememberMachine);

        if (result.Succeeded)
        {
            _logger.LogInformation("User with ID '{UserId}' logged in with 2fa.", user.Id);
            return LocalRedirect(returnUrl);
        }
        else if (result.IsLockedOut)
        {
            _logger.LogWarning("User with ID '{UserId}' account locked out.", user.Id);
            return RedirectToPage("./Lockout");
        }
        else
        {
            _logger.LogWarning("Invalid authenticator code entered for user with ID '{UserId}'.", user.Id);
            ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
            return Page();
        }
    }
}

// Email.cshtml.cs
//
// © 2021 Espresso News. All rights reserved.

using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Espresso.Common.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace Espresso.Dashboard.Areas.Identity.Pages.Account.Manage;

public partial class EmailModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IEmailService _emailService;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailModel"/> class.
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="emailService"></param>
    public EmailModel(
        UserManager<IdentityUser> userManager,
        IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool IsEmailConfirmed { get; set; }

    [TempData]
    public string? StatusMessage { get; set; }

    [BindProperty]
    public InputModel Input { get; set; } = null!;

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

        await LoadAsync(user);
        return Page();
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public async Task<IActionResult> OnPostChangeEmailAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        if (!ModelState.IsValid)
        {
            await LoadAsync(user);
            return Page();
        }

        var email = await _userManager.GetEmailAsync(user);
        if (Input.NewEmail != email)
        {
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmailChange",
                pageHandler: null,
                values: new { userId, email = Input.NewEmail, code },
                protocol: Request.Scheme)!;
            await _emailService.SendMail(
                to: Input.NewEmail,
                subject: "Confirm your email",
                content: $"Please confirm your account by clicking {HtmlEncoder.Default.Encode(callbackUrl)}",
                htmlContent: $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Confirmation link to change email sent. Please check your email.";
            return RedirectToPage();
        }

        StatusMessage = "Your email is unchanged.";
        return RedirectToPage();
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public async Task<IActionResult> OnPostSendVerificationEmailAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        if (!ModelState.IsValid)
        {
            await LoadAsync(user);
            return Page();
        }

        var userId = await _userManager.GetUserIdAsync(user);
        var email = await _userManager.GetEmailAsync(user);
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        var callbackUrl = Url.Page(
            "/Account/ConfirmEmail",
            pageHandler: null,
            values: new { area = "Identity", userId, code },
            protocol: Request.Scheme)!;
        await _emailService.SendMail(
            to: email,
            subject: "Confirm your email",
            content: $"Please confirm your account by clicking {HtmlEncoder.Default.Encode(callbackUrl)}",
            htmlContent: $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

        StatusMessage = "Verification email sent. Please check your email.";
        return RedirectToPage();
    }

    private async Task LoadAsync(IdentityUser user)
    {
        var email = await _userManager.GetEmailAsync(user);
        Email = email;

        Input = new InputModel
        {
            NewEmail = email,
        };

        IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
    }

    public class InputModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "New email")]
        public string NewEmail { get; set; } = null!;
    }
}

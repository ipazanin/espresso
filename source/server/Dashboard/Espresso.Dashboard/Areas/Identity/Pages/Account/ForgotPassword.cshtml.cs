// ForgotPassword.cshtml.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Text.Encodings.Web;
using Espresso.Common.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace Espresso.Dashboard.Areas.Identity.Pages.Account;

[AllowAnonymous]
#pragma warning disable SA1649 // File name should match first type name
public class ForgotPasswordModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IEmailService _emailService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ForgotPasswordModel"/> class.
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="emailService"></param>
    public ForgotPasswordModel(UserManager<IdentityUser> userManager, IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }

    [BindProperty]
    public ForgetPasswordInputModel Input { get; set; } = null!;

    /// <summary>
    ///
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
#pragma warning disable SA1201 // Elements should appear in the correct order
    public async Task<IActionResult> OnPostAsync()
#pragma warning restore SA1201 // Elements should appear in the correct order
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            // For more information on how to enable account confirmation and password reset please
            // visit https://go.microsoft.com/fwlink/?LinkID=532713
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ResetPassword",
                pageHandler: null,
                values: new { area = "Identity", code },
                protocol: Request.Scheme)!;

            await _emailService.SendMail(
                recipient: Input.Email,
                subject: "Reset Password",
                content: $"Please reset your password by clicking {HtmlEncoder.Default.Encode(callbackUrl)}",
                htmlContent: $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            return RedirectToPage("./ForgotPasswordConfirmation");
        }

        return Page();
    }
}

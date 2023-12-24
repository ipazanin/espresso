// ResetPasswordInputModel.cs
//
// © 2022 Espresso News. All rights reserved.

using System.ComponentModel.DataAnnotations;

namespace Espresso.Dashboard.Areas.Identity.Pages.Account;

public class ResetPasswordInputModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = null!;

    public string Code { get; set; } = null!;
}

// LoginInputModel.cs
//
// © 2022 Espresso News. All rights reserved.

using System.ComponentModel.DataAnnotations;

namespace Espresso.Dashboard.Areas.Identity.Pages.Account;

public class LoginInputModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}

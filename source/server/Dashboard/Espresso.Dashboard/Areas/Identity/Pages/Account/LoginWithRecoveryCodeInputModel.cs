// LoginWithRecoveryCodeInputModel.cs
//
// © 2022 Espresso News. All rights reserved.

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Espresso.Dashboard.Areas.Identity.Pages.Account;

public class LoginWithRecoveryCodeInputModel
{
    [BindProperty]
    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Recovery Code")]
    public string RecoveryCode { get; set; } = null!;
}

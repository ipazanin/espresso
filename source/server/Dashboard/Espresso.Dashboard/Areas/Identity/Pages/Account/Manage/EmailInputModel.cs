// EmailInputModel.cs
//
// © 2022 Espresso News. All rights reserved.

using System.ComponentModel.DataAnnotations;

namespace Espresso.Dashboard.Areas.Identity.Pages.Account.Manage;

public class EmailInputModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "New email")]
    public string NewEmail { get; set; } = null!;
}

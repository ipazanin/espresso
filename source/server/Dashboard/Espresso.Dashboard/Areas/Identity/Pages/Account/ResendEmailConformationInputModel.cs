// ResendEmailConformationInputModel.cs
//
// © 2022 Espresso News. All rights reserved.

using System.ComponentModel.DataAnnotations;

namespace Espresso.Dashboard.Areas.Identity.Pages.Account;

public class ResendEmailConformationInputModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
}

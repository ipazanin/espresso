// ManageAccountInputModel.cs
//
// © 2022 Espresso News. All rights reserved.

using System.ComponentModel.DataAnnotations;

namespace Espresso.Dashboard.Areas.Identity.Pages.Account.Manage;

public class ManageAccountInputModel
{
    [Phone]
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; } = null!;
}

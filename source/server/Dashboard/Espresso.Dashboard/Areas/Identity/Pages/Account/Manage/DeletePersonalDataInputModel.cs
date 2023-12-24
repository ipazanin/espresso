// DeletePersonalDataInputModel.cs
//
// © 2022 Espresso News. All rights reserved.

using System.ComponentModel.DataAnnotations;

namespace Espresso.Dashboard.Areas.Identity.Pages.Account.Manage;
public class DeletePersonalDataInputModel
{
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}

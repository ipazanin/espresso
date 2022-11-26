// AppEnvironment.cs
//
// © 2022 Espresso News. All rights reserved.

using System.ComponentModel.DataAnnotations;

namespace Espresso.Common.Enums;

/// <summary>
/// Application environment.
/// </summary>
public enum AppEnvironment
{
    Undefined = 0,
    Local = 1,
    [Display(Name = "Development")]
    Dev = 2,
    [Display(Name = "Production")]
    Prod = 3,
}

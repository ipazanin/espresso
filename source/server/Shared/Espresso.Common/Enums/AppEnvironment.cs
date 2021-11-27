﻿// AppEnvironment.cs
//
// © 2021 Espresso News. All rights reserved.

using System.ComponentModel.DataAnnotations;

namespace Espresso.Common.Enums;

/// <summary>
/// Application environment.
/// </summary>
public enum AppEnvironment
{
#pragma warning disable SA1602 // Enumeration items should be documented
    Undefined = 0,
    Local = 1,
    [Display(Name = "Development")]
    Dev = 2,
    [Display(Name = "Production")]
    Prod = 3,
#pragma warning restore SA1602 // Enumeration items should be documented
}

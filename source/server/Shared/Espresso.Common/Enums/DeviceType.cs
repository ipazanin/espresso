﻿// DeviceType.cs
//
// © 2022 Espresso News. All rights reserved.

using System.ComponentModel.DataAnnotations;

namespace Espresso.Common.Enums;

/// <summary>
/// Device type.
/// </summary>
public enum DeviceType
{
    [Display(Name = "Undefined")]
    Undefined = 0,
    [Display(Name = "Android")]
    Android = 1,
    [Display(Name = "iOS")]
    Ios = 2,
    [Display(Name = "RssFeed Parser")]
    RssFeedParser = 3,
    [Display(Name = "Android Widget")]
    AndroidWidget = 4,
    [Display(Name = "iOS Widget")]
    IosWidget = 5,
    [Display(Name = "Apple Watch")]
    AppleWatch = 6,
    [Display(Name = "Web App")]
    WebApp = 7,
    [Display(Name = "Web Api")]
    WebApi = 8,
}

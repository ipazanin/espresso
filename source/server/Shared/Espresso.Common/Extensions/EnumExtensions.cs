﻿// EnumExtensions.cs
//
// © 2022 Espresso News. All rights reserved.

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Espresso.Common.Constants;

namespace Espresso.Common.Extensions;

/// <summary>
/// <see cref="Enum"/> extensions.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets enum display name or ToString() value if no display name is found.
    /// </summary>
    /// <param name="value">Enum value.</param>
    /// <returns>Display name string.</returns>
    public static string GetDisplayName(this Enum value)
    {
        var type = value.GetType();

        if (!Enum.IsDefined(type, value))
        {
            return FormatConstants.Undefined;
        }

        var members = type.GetMember(value.ToString());
        var display = (DisplayAttribute?)members.FirstOrDefault()?.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();

        return (display?.Name is not null) ?
            display.Name :
            value.ToString();
    }

    /// <summary>
    /// Returns <paramref name="value"/> converted to <see cref="int"/> then to <see cref="string"/>.
    /// </summary>
    /// <param name="value">Enum value.</param>
    /// <returns><paramref name="value"/> converted to <see cref="int"/> then to <see cref="string"/>.</returns>
    public static string GetIntegerValueAsString(this Enum value)
    {
        return Convert.ToInt32(value, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
    }
}

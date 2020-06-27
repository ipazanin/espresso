﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Espresso.Common.Constants;

namespace Espresso.Domain.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets enum display name or ToString() value if no display name is found
        /// </summary>
        /// <param name="value">Enum value</param>
        /// <returns>Display name string</returns>
        public static string GetDisplayName(this Enum value)
        {
            var type = value.GetType();

            if (!Enum.IsDefined(type, value))
            {
                return FormatConstants.Undefined;
            }

            var members = type.GetMember(value.ToString());
            var display = (DisplayAttribute)members.First().GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();

            if (display != null)
            {
                return display.Name;
            }

            return value.ToString();
        }
    }
}

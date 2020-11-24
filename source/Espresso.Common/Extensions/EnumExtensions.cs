using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Espresso.Common.Constants;

namespace Espresso.Common.Extensions
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
            var display = (DisplayAttribute?)members.FirstOrDefault()?.GetCustomAttributes(typeof(DisplayAttribute), false)?.FirstOrDefault();

            return (display is not null && display.Name is not null) ?
                display.Name :
                value.ToString();
        }

        public static string GetIntegerValueAsString(this Enum value)
        {
            return Convert.ToInt32(value).ToString();
        }
    }
}

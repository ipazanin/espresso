// EnumUtility.cs
//
// © 2021 Espresso News. All rights reserved.

using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Espresso.Domain.Utilities
{
    /// <summary>
    /// <see cref="Enum"/> utility.
    /// </summary>
    public static class EnumUtility
    {
        /// <summary>
        /// Tries to create enum from int value.
        /// </summary>
        /// <typeparam name="T"><see cref="Enum"/>.</typeparam>
        /// <param name="enumValue">Enum int value.</param>
        /// <param name="value">Out result.</param>
        /// <returns>is conversion successfull.</returns>
        public static bool TryParseEnum<T>(int enumValue, out T value)
            where T : struct, Enum
        {
            var type = typeof(T);

            value = (T)Enum.Parse(typeof(T), enumValue.ToString(), true);

            return Enum.IsDefined(type, value);
        }

        /// <summary>
        /// Tries to create enum from string value.
        /// </summary>
        /// <typeparam name="T"><see cref="Enum"/>.</typeparam>
        /// <param name="enumValue">Enum string value.</param>
        /// <param name="value">Out result.</param>
        /// <returns>is conversion successfull.</returns>
        public static bool TryParseEnum<T>(string enumValue, out T value)
            where T : struct, Enum
        {
            try
            {
                var type = typeof(T);

                foreach (var field in type.GetFields())
                {
                    if (Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) is DisplayAttribute attribute && attribute.Name == enumValue)
                    {
                        value = (T)field.GetValue(null)!;
                        return true;
                    }
                }

                value = (T)Enum.Parse(typeof(T), enumValue, true);

                return Enum.IsDefined(type, value);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                value = default;
                return false;
            }
        }

        /// <summary>
        /// Gets enum from string or default value if parsing fails.
        /// </summary>
        /// <typeparam name="T"><see cref="Enum"/>.</typeparam>
        /// <param name="enumValue"><see cref="string"/> <see cref="Enum"/> value.</param>
        /// <param name="defaultValue">Default <see cref="Enum"/> value.</param>
        /// <returns>Parsed <see cref="Enum"/> value or <paramref name="defaultValue"/>.</returns>
        public static T GetEnumOrDefault<T>(string enumValue, T defaultValue)
            where T : struct, Enum
        {
            return TryParseEnum<T>(enumValue, out var createdEnum) ? createdEnum : defaultValue;
        }

        /// <summary>
        /// Gets enum from <see cref="int"/> or <paramref name="defaultValue"/> if parsing fails.
        /// </summary>
        /// <typeparam name="T"><see cref="Enum"/>.</typeparam>
        /// <param name="enumValue"><see cref="int"/> <see cref="Enum"/> value.</param>
        /// <param name="defaultValue">Default <see cref="Enum"/> value.</param>
        /// <returns>Parsed <see cref="Enum"/> value or <paramref name="defaultValue"/>.</returns>
        public static T GetEnumOrDefault<T>(int enumValue, T defaultValue)
            where T : struct, Enum
        {
            return TryParseEnum<T>(enumValue, out var createdEnum) ? createdEnum : defaultValue;
        }

        /// <summary>
        /// Gets <paramref name="enumValue"/> if defined or <paramref name="defaultValue"/> if parsing fails.
        /// </summary>
        /// <typeparam name="T"><see cref="Enum"/>.</typeparam>
        /// <param name="enumValue"><see cref="Enum"/> value.</param>
        /// <param name="defaultValue">Default <see cref="Enum"/> value.</param>
        /// <returns><see cref="Enum"/> value if defined or <paramref name="defaultValue"/>.</returns>
        public static T GetEnumOrDefault<T>(T enumValue, T defaultValue)
            where T : struct, Enum
        {
            return Enum.IsDefined(typeof(T), enumValue) ? enumValue : defaultValue;
        }

        /// <summary>
        /// Returns all enum values from <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="Enum"/>.</typeparam>
        /// <returns>All enum values from <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> GetAllValues<T>()
            where T : struct, Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// Gets all <typeparamref name="T"/> <see cref="Enum"/> values except <paramref name="values"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="Enum"/>.</typeparam>
        /// <param name="values">Values to not include in result.</param>
        /// <returns><typeparamref name="T"/> <see cref="Enum"/> values.</returns>
        public static IEnumerable<T> GetAllValuesExcept<T>(IEnumerable<T>? values)
            where T : struct, Enum
        {
            return values?.Any() != true ? GetAllValues<T>() : GetAllValues<T>().Except(values);
        }
    }
}

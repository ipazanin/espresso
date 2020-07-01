using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace Espresso.Domain.Utilities
{
    public static class EnumUtility
    {
        /// <summary>
        /// Tries to create enum from int value
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        /// <param name="enumValue">Enum int value</param>
        /// <param name="value">Out result</param>
        /// <returns>is conversion successfull</returns>
        public static bool TryParseEnum<T>(int enumValue, out T value) where T : struct, IConvertible
        {
            var type = typeof(T);

            value = (T)Enum.Parse(typeof(T), enumValue.ToString(), true);

            if (Enum.IsDefined(type, value))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Tries to create enum from string value
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        /// <param name="enumValue">Enum string value</param>
        /// <param name="value">Out result</param>
        /// <returns>is conversion successfull</returns>
        public static bool TryParseEnum<T>(string enumValue, out T value) where T : struct, IConvertible
        {
            try
            {
                var type = typeof(T);

                foreach (var field in type.GetFields())
                {
                    if (Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) is DisplayAttribute attribute)
                    {
                        if (attribute.Name == enumValue)
                        {
                            value = (T)field.GetValue(null)!;
                            return true;
                        }
                    }
                }

                value = (T)Enum.Parse(typeof(T), enumValue, true);

                if (Enum.IsDefined(type, value))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                value = default;
                return false;
            }
        }

        /// <summary>
        /// Gets enum from string or default value if parsing fails
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        /// <param name="enumValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetEnumOrDefault<T>(string enumValue, T defaultValue) where T : struct, IConvertible
        {
            if (TryParseEnum<T>(enumValue, out var createdEnum))
            {
                return createdEnum;
            }

            return defaultValue;
        }

        /// <summary>
        /// Gets enum from int or default value if parsing fails
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        /// <param name="enumValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetEnumOrDefault<T>(int enumValue, T defaultValue) where T : struct, IConvertible
        {
            if (TryParseEnum<T>(enumValue, out var createdEnum))
            {
                return createdEnum;
            }

            return defaultValue;
        }

        /// <summary>
        /// Gets enum if defined or default value if parsing fails
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        /// <param name="enumValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetEnumOrDefault<T>(T enumValue, T defaultValue) where T : struct, IConvertible
        {
            if (Enum.IsDefined(typeof(T), enumValue))
            {
                return enumValue;
            }

            return defaultValue;
        }

        public static IEnumerable<T> GetAllValues<T>() where T : struct, IConvertible
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static IEnumerable<T> GetAllValuesExcept<T>(IEnumerable<T>? values) where T : struct, IConvertible
        {
            return values == null || values.Any() ? GetAllValues<T>().Except(values) : GetAllValues<T>();
        }
    }
}

// GraphQlUserContext.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Enums;
using System.Collections.Generic;

namespace Espresso.WebApi.GraphQl.Infrastructure
{
    /// <summary>
    ///
    /// </summary>
#pragma warning disable S3925 // "ISerializable" should be implemented correctly
    public class GraphQlUserContext : Dictionary<string, object?>
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
    {
        private const string TargetedApiVersionKey = nameof(TargetedApiVersionKey);
        private const string ConsumerVersionKey = nameof(ConsumerVersionKey);
        private const string DeviceTypeKey = nameof(DeviceTypeKey);

        /// <summary>
        ///
        /// </summary>
        public string TargetedApiVersion
        {
            get
            {
                if (!TryGetValue(TargetedApiVersionKey, out var value))
                {
                    return string.Empty;
                }

                return value?.ToString() ?? string.Empty;
            }

            set
            {
                TryAdd(TargetedApiVersionKey, value ?? string.Empty);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public string ConsumerVersion
        {
            get
            {
                if (!TryGetValue(ConsumerVersionKey, out var value))
                {
                    return string.Empty;
                }

                return value?.ToString() ?? string.Empty;
            }

            set
            {
                TryAdd(ConsumerVersionKey, value ?? string.Empty);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public DeviceType DeviceType
        {
            get
            {
                if (!TryGetValue(DeviceTypeKey, out var value))
                {
                    return DeviceType.Undefined;
                }

                return value is null ? DeviceType.Undefined : (DeviceType)value;
            }

            set
            {
                TryAdd(DeviceTypeKey, value);
            }
        }
    }
}

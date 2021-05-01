using System.Collections.Generic;
using Espresso.Common.Enums;

namespace Espresso.WebApi.GraphQl.Infrastructure
{
    /// <summary>
    ///
    /// </summary>
#pragma warning disable S3925 // "ISerializable" should be implemented correctly
    public class GraphQlUserContext : Dictionary<string, object>
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
    {
        #region Constants
        private const string TargetedApiVersionKey = nameof(TargetedApiVersionKey);
        private const string ConsumerVersionKey = nameof(ConsumerVersionKey);
        private const string DeviceTypeKey = nameof(DeviceTypeKey);
        #endregion

        #region Properties
        /// <summary>
        ///
        /// </summary>
        /// <value></value>
        public string TargetedApiVersion
        {
            get
            {
                if (!TryGetValue(TargetedApiVersionKey, out var value))
                {
                    return string.Empty;
                }

                return (string)value;
            }

            set
            {
                TryAdd(TargetedApiVersionKey, value ?? string.Empty);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <value></value>
        public string ConsumerVersion
        {
            get
            {
                if (!TryGetValue(ConsumerVersionKey, out var value))
                {
                    return string.Empty;
                }

                return (string)value;
            }

            set
            {
                TryAdd(ConsumerVersionKey, value ?? string.Empty);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <value></value>
        public DeviceType DeviceType
        {
            get
            {
                if (!TryGetValue(DeviceTypeKey, out var value))
                {
                    return DeviceType.Undefined;
                }

                return (DeviceType)value;
            }

            set
            {
                TryAdd(DeviceTypeKey, value);
            }
        }
        #endregion

    }
}

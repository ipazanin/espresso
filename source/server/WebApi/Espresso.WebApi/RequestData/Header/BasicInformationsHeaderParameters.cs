// BasicInformationsHeaderParameters.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Espresso.WebApi.RequestData.Header
{
    /// <summary>
    /// 
    /// </summary>
    public class BasicInformationsHeaderParameters
    {
        /// <summary>
        /// 
        /// </summary>
        [FromHeader(Name = HttpHeaderConstants.ApiVersionHeaderName)]
        public string EspressoWebApiVersion { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [FromHeader(Name = HttpHeaderConstants.VersionHeaderName)]
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [FromHeader(Name = HttpHeaderConstants.DeviceTypeHeaderName)]
        public DeviceType DeviceType { get; set; } = DeviceType.Undefined;
    }
}

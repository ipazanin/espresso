using Espresso.Common.Constants;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Microsoft.AspNetCore.Mvc;

namespace Espresso.WebApi.Parameters.HeaderParameters
{
    /// <summary>
    /// 
    /// </summary>
    public class BasicInformationsHeaderParameters
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        [FromHeader(Name = HttpHeaderConstants.EspressoApiHeaderName)]
        public string EspressoWebApiVersion { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [FromHeader(Name = HttpHeaderConstants.VersionHeaderName)]
        public string Version { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [FromHeader(Name = HttpHeaderConstants.DeviceTypeHeaderName)]
        public DeviceType DeviceType { get; set; } = DeviceType.Undefined;
        #endregion
    }
}

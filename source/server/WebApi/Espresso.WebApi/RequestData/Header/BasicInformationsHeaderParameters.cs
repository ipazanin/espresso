// BasicInformationsHeaderParameters.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Espresso.WebApi.RequestData.Header;

public class BasicInformationsHeaderParameters
{
    [FromHeader(Name = HttpHeaderConstants.ApiVersionHeaderName)]
    public string EspressoWebApiVersion { get; set; } = string.Empty;

    [FromHeader(Name = HttpHeaderConstants.VersionHeaderName)]
    public string Version { get; set; } = string.Empty;

    [FromHeader(Name = HttpHeaderConstants.DeviceTypeHeaderName)]
    public DeviceType DeviceType { get; set; } = DeviceType.Undefined;
}

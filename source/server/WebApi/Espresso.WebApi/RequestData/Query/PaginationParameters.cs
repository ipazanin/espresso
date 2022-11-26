// PaginationParameters.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.AspNetCore.Mvc;

namespace Espresso.WebApi.RequestData.Query;

/// <summary>
///
/// </summary>
public class PaginationParameters
{
    /// <summary>
    ///
    /// </summary>
    [FromQuery(Name = "take")]
    public int Take { get; set; } = 20;

    /// <summary>
    ///
    /// </summary>
    [FromQuery(Name = "skip")]
    public int Skip { get; set; } = 0;
}

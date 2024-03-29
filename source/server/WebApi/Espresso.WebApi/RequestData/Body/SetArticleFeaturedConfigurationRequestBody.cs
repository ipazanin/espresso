﻿// SetArticleFeaturedConfigurationRequestBody.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.WebApi.RequestData.Body;

/// <summary>
///
/// </summary>
public record SetArticleFeaturedConfigurationRequestBody
{
    /// <summary>
    ///
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    ///
    /// </summary>
    public bool? IsFeatured { get; init; }

    /// <summary>
    ///
    /// </summary>
    public int? FeaturedPosition { get; init; }
}

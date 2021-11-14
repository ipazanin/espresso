// SetArticleFeaturedConfigurationRequestBody.cs
//
// © 2021 Espresso News. All rights reserved.

using System;

namespace Espresso.WebApi.RequestData.Body
{
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
}

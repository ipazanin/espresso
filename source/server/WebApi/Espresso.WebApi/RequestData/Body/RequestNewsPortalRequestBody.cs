// RequestNewsPortalRequestBody.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.WebApi.RequestData.Body
{
    /// <summary>
    ///
    /// </summary>
    public record RequestNewsPortalRequestBody
    {
        /// <summary>
        ///
        /// </summary>
        public string? NewsPortalName { get; init; }

        /// <summary>
        ///
        /// </summary>
        public string? Email { get; init; }

        /// <summary>
        ///
        /// </summary>
        public string? Url { get; init; }

    }
}

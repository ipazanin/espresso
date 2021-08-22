// HealthCheckConstants.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Common.Constants
{
    /// <summary>
    /// Health check constants.
    /// </summary>
    public static class HealthCheckConstants
    {
        /// <summary>
        /// Startup tag.
        /// </summary>
        public const string StartupTag = nameof(StartupTag);

        /// <summary>
        /// Readiness tag.
        /// </summary>
        public const string ReadinessTag = nameof(ReadinessTag);

        /// <summary>
        /// Liveness tag.
        /// </summary>
        public const string LivenessTag = nameof(LivenessTag);
    }
}

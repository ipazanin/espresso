// ReadinessHealthCheck.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Espresso.WebApi.Application.HealthChecks
{
    /// <summary>
    /// Health check to verify app is ready to receive requests.
    /// </summary>
    public class ReadinessHealthCheck : IHealthCheck
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadinessHealthCheck"/> class.
        /// </summary>
        public ReadinessHealthCheck()
        {
        }

        /// <inheritdoc/>
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (ReadinessTaskCompleted)
            {
                return Task.FromResult(HealthCheckResult.Healthy("Application is ready to accept requests."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("Application is not ready to accept requests."));
        }

        /// <summary>
        /// Gets or sets a value indicating whether readiness task is completed.
        /// </summary>
#pragma warning disable SA1201 // Elements should appear in the correct order
        public bool ReadinessTaskCompleted { get; set; } = false;
#pragma warning restore SA1201 // Elements should appear in the correct order
    }
}

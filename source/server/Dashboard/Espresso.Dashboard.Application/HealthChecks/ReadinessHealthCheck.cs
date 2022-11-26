// ReadinessHealthCheck.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Espresso.Dashboard.Application.HealthChecks;

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

    /// <summary>
    /// Gets or sets a value indicating whether readiness task is completed.
    /// </summary>
    /// <remarks>
    /// TODO: Can this be done without property mutation?.
    /// </remarks>
    public bool ReadinessTaskCompleted { get; set; }

    /// <inheritdoc/>
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return ReadinessTaskCompleted ?
            Task.FromResult(HealthCheckResult.Healthy("Application is ready to accept requests.")) :
            Task.FromResult(HealthCheckResult.Unhealthy("Application is not ready to accept requests."));
    }
}

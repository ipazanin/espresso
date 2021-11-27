// StartupHealthCheck.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Espresso.WebApi.Application.HealthChecks;

/// <summary>
/// Health check to disables liveness and readiness checks until it succeeds, making sure those probes don't interfere with the application startup.
/// </summary>
public class StartupHealthCheck : IHealthCheck
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StartupHealthCheck "/> class.
    /// </summary>
    public StartupHealthCheck()
    {
    }

    /// <inheritdoc/>
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HealthCheckResult.Healthy("Application startup is healthy"));
    }
}

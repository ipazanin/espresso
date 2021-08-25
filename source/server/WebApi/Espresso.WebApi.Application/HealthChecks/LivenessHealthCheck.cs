﻿// LivenessHealthCheck.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace Espresso.WebApi.Application.HealthChecks
{
    /// <summary>
    /// Health check to know when to restart a container.
    /// </summary>
    public class LivenessHealthCheck : IHealthCheck
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LivenessHealthCheck"/> class.
        /// </summary>
        public LivenessHealthCheck()
        {
        }

        /// <inheritdoc/>
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(HealthCheckResult.Healthy("Application is live"));
        }
    }
}

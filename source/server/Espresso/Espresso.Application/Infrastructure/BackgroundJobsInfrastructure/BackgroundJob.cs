// BackgroundJob.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Domain.IServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Espresso.Application.Infrastructure.BackgroundJobsInfrastructure
{
    /// <summary>
    /// Long running background job.
    /// </summary>
    /// <typeparam name="T">Background job, implementing <see cref="BackgroundJob{T}"/>.</typeparam>
    public abstract class BackgroundJob<T> : IHostedService
        where T : BackgroundJob<T>
    {
        /// <summary>
        /// Gets service scope factory.
        /// </summary>
        protected IServiceScopeFactory ServiceScopeFactory { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundJob{T}"/> class.
        /// </summary>
        /// <param name="serviceScopeFactory">Service scope factory.</param>
        protected BackgroundJob(
            IServiceScopeFactory serviceScopeFactory
        )
        {
            ServiceScopeFactory = serviceScopeFactory;
        }

        /// <inheritdoc />
        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = ServiceScopeFactory.CreateScope();
            var loggerService = scope.ServiceProvider.GetRequiredService<ILoggerService<BackgroundJob<T>>>();

            var jobName = typeof(T).Name;

            loggerService.Log(
                eventName: $"{jobName} is starting",
                logLevel: LogLevel.Information
            );

            try
            {
                await DoWork(cancellationToken);
            }
            catch (Exception exception)
            {
                var errorEventName = $"Error while working {jobName}";
                loggerService.Log(
                    eventName: errorEventName,
                    exception: exception,
                    logLevel: LogLevel.Error
                );
            }
        }

        /// <summary>
        /// Background job work.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="Task"/>Representing the result of the asynchronous operation.</returns>
        public abstract Task DoWork(CancellationToken cancellationToken);

        /// <inheritdoc/>
#pragma warning disable RCS1229 // Use async/await when necessary
        public virtual Task StopAsync(CancellationToken cancellationToken)
#pragma warning restore RCS1229
        {
            using var scope = ServiceScopeFactory.CreateScope();
            var loggerService = scope.ServiceProvider.GetRequiredService<ILoggerService<BackgroundJob<T>>>();

            var eventName = $"{typeof(T).Name} stopped";
            loggerService.Log(eventName, LogLevel.Information);

            return Task.CompletedTask;
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Domain.IServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Espresso.Application.Infrastructure.BackgroundJobsInfrastructure
{
    public abstract class BackgroundJob<T> : IHostedService
        where T : BackgroundJob<T>
    {
        #region Properties

        protected IServiceScopeFactory ServiceScopeFactory { get; }

        #endregion

        #region Constructors

        protected BackgroundJob(
            IServiceScopeFactory serviceScopeFactory
        )
        {
            ServiceScopeFactory = serviceScopeFactory;
        }

        #endregion

        #region Methods

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

        public abstract Task DoWork(CancellationToken cancellationToken);

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

        #endregion
    }
}

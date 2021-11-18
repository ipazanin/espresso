// CronJob.cs
//
// © 2021 Espresso News. All rights reserved.

using Cronos;
using Espresso.Application.Services.Contracts;
using Espresso.Domain.IServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Timer = System.Timers.Timer;

namespace Espresso.Application.Infrastructure.CronJobsInfrastructure
{
    /// <summary>
    /// Represents scheduled background job.
    /// </summary>
    /// <typeparam name="T">Cron job.</typeparam>
    public abstract class CronJob<T> : IHostedService, IDisposable
        where T : CronJob<T>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private Timer? _timer;
        private bool _disposedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="CronJob{T}"/> class.
        /// </summary>
        /// <param name="cronJobConfiguration">Cron job configuration.</param>
        /// <param name="serviceScopeFactory">Service scope factory.</param>
        protected CronJob(
            ICronJobConfiguration<T> cronJobConfiguration,
            IServiceScopeFactory serviceScopeFactory)
        {
            CronJobConfiguration = cronJobConfiguration;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected ICronJobConfiguration<T> CronJobConfiguration { get; }

        protected abstract CronExpression CronExpression { get; }

        /// <inheritdoc />
        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            return ScheduleJob(cancellationToken);
        }

        /// <summary>
        /// Cron job work.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public abstract Task DoWork(CancellationToken cancellationToken);

        /// <inheritdoc />
#pragma warning disable RCS1229 // Use async/await when necessary
        public virtual Task StopAsync(CancellationToken cancellationToken)
#pragma warning restore RCS1229
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var loggerService = scope.ServiceProvider.GetRequiredService<ILoggerService<CronJob<T>>>();

            var eventName = $"{typeof(T).Name} stopped";
            loggerService.Log(eventName, LogLevel.Information);

            _timer?.Stop();

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Schedules cron job.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        protected virtual async Task ScheduleJob(CancellationToken cancellationToken)
        {
            var occurrence = CronExpression.GetNextOccurrence(
                from: DateTimeOffset.Now,
                zone: CronJobConfiguration.TimeZoneInfo);

            if (!occurrence.HasValue)
            {
                return;
            }

            var delay = occurrence.Value - DateTimeOffset.Now;

            if (delay.TotalMilliseconds <= 0)
            {
                await ScheduleJob(cancellationToken);
            }

            _timer = new Timer(delay.TotalMilliseconds);
#pragma warning disable AsyncFixer03, VSTHRD101 // Avoid unsupported fire-and-forget async-void methods or delegates. Unhandled exceptions will crash the process
            _timer.Elapsed += async (sender, args) =>
            {
                _timer?.Dispose();
                _timer = null;

                await ExecuteWork(
                    occurrence: occurrence.Value,
                    cancellationToken: cancellationToken);
            };
#pragma warning restore AsyncFixer03, VSTHRD101
            _timer.Start();

            var eventName = $"{typeof(T).Name} scheduled";
            var arguments = new List<(string argumentName, object argumentValue)>
            {
                (nameof(occurrence), occurrence),
            };

            using var scope = _serviceScopeFactory.CreateScope();
            var loggerService = scope.ServiceProvider.GetRequiredService<ILoggerService<CronJob<T>>>();

            loggerService.Log(eventName, LogLevel.Information, arguments);
        }

        /// <summary>
        /// Performs application defined tasks associated with freeing,
        /// releasing, or reseting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Is disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _timer?.Dispose();
                }

                _disposedValue = true;
            }
        }

        private async Task ExecuteWork(
            DateTimeOffset occurrence,
            CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            using var scope = _serviceScopeFactory.CreateScope();
            var loggerService = scope.ServiceProvider.GetRequiredService<ILoggerService<CronJob<T>>>();

            try
            {
                var stopwatch = Stopwatch.StartNew();
                await DoWork(cancellationToken);
                var elapsed = stopwatch.Elapsed;

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);

                var eventName = $"{typeof(T).Name} work ended";
                var nextOccurrence = CronExpression.GetNextOccurrence(DateTimeOffset.Now, CronJobConfiguration.TimeZoneInfo) ?? occurrence;

                var arguments = new (string argumentName, object argumentValue)[]
                {
                    (nameof(occurrence), occurrence),
                    (nameof(nextOccurrence), nextOccurrence),
                    (nameof(elapsed), elapsed),
                };
                loggerService.Log(eventName, LogLevel.Information, arguments);
            }
            catch (Exception exception)
            {
                var eventName = $"{typeof(T).Name} error";

                var arguments = new (string argumentName, object argumentValue)[]
                {
                    (nameof(occurrence), occurrence),
                };

                loggerService.Log(eventName, exception, LogLevel.Error, arguments);

                var slackService = scope.ServiceProvider.GetRequiredService<ISlackService>();
                await slackService.LogError(
                    eventName: eventName,
                    message: exception.Message,
                    exception: exception,
                    cancellationToken: default);
            }

            if (!cancellationToken.IsCancellationRequested)
            {
                await ScheduleJob(cancellationToken);
            }
        }
    }
}

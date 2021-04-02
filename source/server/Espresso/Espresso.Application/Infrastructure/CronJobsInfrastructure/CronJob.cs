using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Cronos;
using Espresso.Application.Services.Contracts;
using Espresso.Domain.IServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Timer = System.Timers.Timer;

namespace Espresso.Application.Infrastructure.CronJobsInfrastructure
{
    public abstract class CronJob<T> : IHostedService, IDisposable where T : CronJob<T>
    {
        #region Fields
        private Timer? _timer;
        private readonly CronExpression _expression;
        private readonly ICronJobConfiguration<T> _cronJobConfiguration;
        protected readonly IServiceScopeFactory ServiceScopeFactory;
        #endregion

        #region Constructors
        protected CronJob(
            ICronJobConfiguration<T> cronJobConfiguration,
            IServiceScopeFactory serviceScopeFactory
        )
        {
            _expression = CronExpression.Parse(cronJobConfiguration.CronExpression);
            _cronJobConfiguration = cronJobConfiguration;
            ServiceScopeFactory = serviceScopeFactory;
        }
        #endregion

        #region Methods
        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {
            await ScheduleJob(cancellationToken);
        }

        protected virtual async Task ScheduleJob(CancellationToken cancellationToken)
        {
            var occurrence = _expression.GetNextOccurrence(
                from: DateTimeOffset.Now,
                zone: _cronJobConfiguration.TimeZoneInfo
            );

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
            _timer.Elapsed += async (sender, args) =>
            {
                _timer?.Dispose();
                _timer = null;

                await ExecuteWork(
                    occurrence: occurrence.Value,
                    cancellationToken: cancellationToken
                );
            };
            _timer.Start();

            var eventName = $"{typeof(T).Name} scheduled";
            var arguments = new List<(string argumentName, object argumentValue)>
            {
                (nameof(occurrence), occurrence)
            };

            using var scope = ServiceScopeFactory.CreateScope();
            var loggerService = scope.ServiceProvider.GetRequiredService<ILoggerService<CronJob<T>>>();

            loggerService.Log(eventName, LogLevel.Information, arguments);
        }

        public abstract Task DoWork(CancellationToken cancellationToken);

        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            using var scope = ServiceScopeFactory.CreateScope();
            var loggerService = scope.ServiceProvider.GetRequiredService<ILoggerService<CronJob<T>>>();

            var eventName = $"{typeof(T).Name} stopped";
            loggerService.Log(eventName, LogLevel.Information);

            _timer?.Stop();

            return Task.CompletedTask;
        }

        private async Task ExecuteWork(
            DateTimeOffset occurrence,
            CancellationToken cancellationToken
        )
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            using var scope = ServiceScopeFactory.CreateScope();
            var loggerService = scope.ServiceProvider.GetRequiredService<ILoggerService<CronJob<T>>>();

            try
            {
                var stopwatch = Stopwatch.StartNew();
                await DoWork(cancellationToken);
                var elapsed = stopwatch.Elapsed;

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);

                var eventName = $"{typeof(T).Name} work ended";
                var nextOccurrence = _expression.GetNextOccurrence(DateTimeOffset.Now, _cronJobConfiguration.TimeZoneInfo) ?? occurrence;

                var arguments = new (string argumentName, object argumentValue)[]
                {
                    (nameof(occurrence),occurrence),
                    (nameof(nextOccurrence),nextOccurrence),
                    (nameof(elapsed),elapsed),
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
                    cancellationToken: default
                );
            }

            if (!cancellationToken.IsCancellationRequested)
            {
                await ScheduleJob(cancellationToken);
            }
        }

        public virtual void Dispose()
        {
            _timer?.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }

}


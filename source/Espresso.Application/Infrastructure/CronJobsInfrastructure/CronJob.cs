using System;
using System.Threading;
using System.Threading.Tasks;
using Cronos;
using Espresso.Application.IServices;
using Espresso.Common.Enums;
using Espresso.Common.Utilities;
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
        private readonly ILogger<T> _logger;
        private readonly ICronJobConfiguration<T> _cronJobConfiguration;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        #endregion

        #region Constructors
        protected CronJob(
            ICronJobConfiguration<T> cronJobConfiguration,
            ILoggerFactory loggerFactory,
            IServiceScopeFactory serviceScopeFactory
        )
        {
            _expression = CronExpression.Parse(cronJobConfiguration.CronExpression);
            _logger = loggerFactory.CreateLogger<T>();
            _cronJobConfiguration = cronJobConfiguration;
            _serviceScopeFactory = serviceScopeFactory;
        }
        #endregion

        #region Methods
        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {
            await ScheduleJob(cancellationToken);
        }

        protected virtual async Task ScheduleJob(CancellationToken cancellationToken)
        {
            var ocurrence = _expression.GetNextOccurrence(
                from: DateTimeOffset.Now,
                zone: _cronJobConfiguration.TimeZoneInfo
            );

            if (!ocurrence.HasValue)
            {
                return;
            }

            var delay = ocurrence.Value - DateTimeOffset.Now;

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
                    ocurrence: ocurrence.Value,
                    cancellationToken: cancellationToken
                );
            };
            _timer.Start();

            var eventName = $"{typeof(T).Name} scheduled";
            _logger.LogInformation(
                eventId: new EventId(
                    id: (int)Event.CronJob,
                    name: eventName
                ),
                message: $"{AnsiUtility.EncodeEventName("{0}")}\n\t" +
                    $"{AnsiUtility.EncodeParameter(nameof(ocurrence))}: " +
                    $"{AnsiUtility.EncodeDateTime("{1}")}",
                args: new object[] { eventName, ocurrence.Value }
            );
        }

        public abstract Task DoWork(CancellationToken cancellationToken);

        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            var eventName = $"{typeof(T).Name} stopped";
            _logger.LogInformation(
                eventId: new EventId(
                    id: (int)Event.CronJob,
                    name: eventName
                ),
                message: $"{AnsiUtility.EncodeEventName("{0}")}",
                args: new object[] { eventName }
            );

            _timer?.Stop();

            return Task.CompletedTask;
        }

        private async Task ExecuteWork(
            DateTimeOffset ocurrence,
            CancellationToken cancellationToken
        )
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            try
            {
                await DoWork(cancellationToken);
                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);

                var eventName = $"{typeof(T).Name} work ended";
                var nextOccurence = _expression.GetNextOccurrence(DateTimeOffset.Now, _cronJobConfiguration.TimeZoneInfo) ?? ocurrence;
                _logger.LogInformation(
                    eventId: new EventId(
                        id: (int)Event.CronJob,
                        name: eventName
                    ),
                    message: $"{AnsiUtility.EncodeEventName("{0}")}\n\t" +
                        $"{AnsiUtility.EncodeParameterName(nameof(ocurrence))}: " +
                        $"{AnsiUtility.EncodeDateTime("{1}")}\n\t" +
                        $"{AnsiUtility.EncodeParameterName(nameof(nextOccurence))}: " +
                        $"{AnsiUtility.EncodeDateTime("{2}")}\n\t",
                    args: new object[] { eventName, ocurrence, nextOccurence }
                );
            }
            catch (Exception exception)
            {
                var eventName = $"{typeof(T).Name} error";
                var exceptionMessage = exception.Message;
                var innerExceptionMessage = exception.InnerException?.Message ?? "";
                _logger.LogError(
                    eventId: new EventId(
                        id: (int)Event.CronJob,
                        name: eventName
                    ),
                    exception: exception,
                    message: $"{AnsiUtility.EncodeEventName("{0}")}\n\t" +
                         $"{AnsiUtility.EncodeParameterName(nameof(ocurrence))}: " +
                         $"{AnsiUtility.EncodeDateTime("{1}")}\n\t" +
                         $"{AnsiUtility.EncodeParameterName(nameof(exceptionMessage))}: " +
                         $"{AnsiUtility.EncodeErrorMessage("{2}")}\n\t" +
                         $"{AnsiUtility.EncodeParameterName(nameof(innerExceptionMessage))}: " +
                         $"{AnsiUtility.EncodeErrorMessage("{3}")}",
                    args: new object[] { eventName, ocurrence, exceptionMessage, innerExceptionMessage }
                );

                using var scope = _serviceScopeFactory.CreateScope();
                var slackService = scope.ServiceProvider.GetRequiredService<ISlackService>();
                await slackService.LogError(
                    eventName: eventName,
                    version: _cronJobConfiguration.Version,
                    message: exception.Message,
                    exception: exception,
                    appEnvironment: _cronJobConfiguration.AppEnvironment,
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

